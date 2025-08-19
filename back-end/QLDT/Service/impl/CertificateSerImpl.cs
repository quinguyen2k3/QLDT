using AutoMapper;
using QLDT.Config;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Models;
using QLDT.Repository;

namespace QLDT.Service.impl
{
    public class CertificateSerImpl : CertificateSer
    {
        private readonly CertificateRepo _certificateRepository;
        private readonly FileCertificateRepo _filecertificateRepository;
        private readonly IMapper _mapper;
        private readonly FileConfig _fileConfig;
        private readonly TransactionManager _transactionManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CertificateSerImpl(CertificateRepo certificateRepository,
                     FileCertificateRepo filecertificateRepository,
                     IMapper mapper,
                     TransactionManager transactionManager,
                     FileConfig fileConfig,
                     IHttpContextAccessor httpContextAccessor)
        {
            _certificateRepository = certificateRepository;
            _filecertificateRepository = filecertificateRepository;
            _mapper = mapper;
            _transactionManager = transactionManager;
            _fileConfig = fileConfig;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<CertificateRes>> GetAllByUserAsync(long? id = null)
        {
            long empId;

            if (id.HasValue)
            {
                empId = id.Value;
            }
            else
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var empIdStr = user?.FindFirst("emp")?.Value;
                if (string.IsNullOrEmpty(empIdStr))
                    throw new UnauthorizedAccessException("Invalid user info in token.");
                if (!long.TryParse(empIdStr, out empId))
                    throw new ArgumentException("Employee Id invalid.");
            }

            var cetificates = await _certificateRepository.GetAllByEmployeeIdAsync(empId);
            var cetificateResList = _mapper.Map<IEnumerable<CertificateRes>>(cetificates);
            return cetificateResList;
        }

        public async Task<CertificateRes> CreateAsync(CertificateReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {

                var certificate = _mapper.Map<Certificate>(request);

                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                var empIdStr = user?.FindFirst("emp")?.Value;
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(empIdStr) || !long.TryParse(empIdStr, out long empId))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                certificate.CreatedDate = DateTime.Now;
                certificate.CreatedBy = username;
                certificate.ModifiedDate = certificate.CreatedDate;
                certificate.ModifiedBy = certificate.CreatedBy;
                certificate.EmpId = empId;

                var createdCertificate = await _certificateRepository.CreateAsync(certificate);

                if (request.Attachments != null && request.Attachments.Any())
                {
                    var fileCetificates = new List<FileCertificate>();

                    var uploadFolder = _fileConfig.UploadBasePath;

                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    foreach (var file in request.Attachments)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(_fileConfig.UploadBasePath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        fileCetificates.Add(new FileCertificate
                        {
                            Id = Guid.NewGuid().ToString(),
                            FileName = fileName,
                            Path = filePath,
                            CertificateId = createdCertificate.Id
                        });
                    }

                    await _filecertificateRepository.SaveAllAsync(fileCetificates);
                    createdCertificate.FileCertificates = fileCetificates;
                }

                await _transactionManager.CommitAsync();

                var cetificateRes = _mapper.Map<CertificateRes>(createdCertificate);
                return cetificateRes;
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error creating certificate: " + ex.Message, ex);
            }
        }

        public async Task<CertificateRes?> GetByIdAsync(long id)
        {
            var entity = await _certificateRepository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<CertificateRes>(entity);
        }

        public async Task<CertificateRes> UpdateAsync(long id, CertificateReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var existingCetificate = await _certificateRepository.GetByIdAsync(id);
                if (existingCetificate == null)
                    throw new Exception("Certificate does not exist!");

                existingCetificate = _mapper.Map(request, existingCetificate);
                existingCetificate.ModifiedDate = DateTime.Now;
                existingCetificate.CreatedBy = username;

                var currentFiles = await _filecertificateRepository.GetByCetificateIdAsync(id);
                var filesToDelete = currentFiles
                    .Where(f => request.OldFileIds == null || !request.OldFileIds.Contains(f.Id))
                    .ToList();

                if (filesToDelete.Any())
                {
                    await _filecertificateRepository.DeleteByIdsAsync(filesToDelete.Select(f => f.Id).ToList());
                }

                if (request.Attachments != null && request.Attachments.Any())
                {
                    var fileCetificate = new List<FileCertificate>();
                    var uploadFolder = _fileConfig.UploadBasePath;

                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    foreach (var file in request.Attachments)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(uploadFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        fileCetificate.Add(new FileCertificate
                        {
                            Id = Guid.NewGuid().ToString(),
                            FileName = fileName,
                            Path = filePath,
                            CertificateId = existingCetificate.Id
                        });
                    }

                    await _filecertificateRepository.SaveAllAsync(fileCetificate);
                }

                var updatedCetificate = await _certificateRepository.UpdateAsync(existingCetificate);

                await _transactionManager.CommitAsync();
                return _mapper.Map<CertificateRes>(updatedCetificate);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error updating course: " + ex.Message, ex);
            }
        }
    }
}
