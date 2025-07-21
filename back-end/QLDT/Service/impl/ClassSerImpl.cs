using AutoMapper;
using QLDT.Config;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Models;
using QLDT.Repository;

namespace QLDT.Service.impl
{
    public class ClassSerImpl : ClassSer
    {
        private readonly ClassRepo _classRepository;
        private readonly FileClassesRepo _fileclassRepository;
        private readonly DetailRepo _detailRepository;
        private readonly IMapper _mapper;
        private readonly FileConfig _fileConfig;
        private readonly TransactionManager _transactionManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClassSerImpl(ClassRepo classRepository,
                     FileClassesRepo fileclassRepository,
                     DetailRepo detailRepository,
                     IMapper mapper,
                     TransactionManager transactionManager,
                     FileConfig fileConfig,
                     IHttpContextAccessor httpContextAccessor)
        {
            _classRepository = classRepository;
            _fileclassRepository = fileclassRepository;
            _detailRepository = detailRepository;
            _mapper = mapper;
            _transactionManager = transactionManager;
            _fileConfig = fileConfig;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<ClassRes>> GetAllAsync(long id)
        {
            var courses = await _classRepository.GetAllByTrainingFormatIdAsync(id);

            var courseResList = _mapper.Map<IEnumerable<ClassRes>>(courses);

            return courseResList;
        }

        public async Task<ClassRes?> GetByIdAsync(long id)
        {
            var entity = await _classRepository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<ClassRes>(entity);
        }

        public async Task<ClassRes> CreateAsync(ClassReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var entity = _mapper.Map<Class>(request);

                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = username;
                entity.ModifiedDate = entity.CreatedDate;
                entity.ModifiedBy = entity.CreatedBy;

                var createdClass = await _classRepository.SaveAsync(entity);

                if (request.Attachments != null && request.Attachments.Any())
                {
                    var fileClasses = new List<FileClass>();
                    var uploadFolder = _fileConfig.UploadBasePath;

                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    foreach (var file in request.Attachments)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(uploadFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        fileClasses.Add(new FileClass
                        {
                            Id = Guid.NewGuid().ToString(),
                            FileName = fileName,
                            Path = filePath,
                            ClassId = createdClass.Id
                        });
                    }

                    await _fileclassRepository.SaveAllAsync(fileClasses);
                    createdClass.FileClasses = fileClasses;
                }

                if (request.EmployeeIds != null && request.EmployeeIds.Any())
                {
                    var classEmployees = request.EmployeeIds.Select(empId => new Detail
                    {
                        ClassId = createdClass.Id,
                        EmpId = empId,
                        SoTinhChi = request.SoTinhChi
                    }).ToList();

                    await _detailRepository.SaveAllAsync(classEmployees);
                }

                await _transactionManager.CommitAsync();

                var classRes = _mapper.Map<ClassRes>(createdClass);
                return classRes;
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error creating class: " + ex.Message, ex);
            }
        }

        public async  Task<ClassRes> UpdateAsync(long id, ClassReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var existingClass = await _classRepository.GetByIdAsync(id);
                if (existingClass == null)
                    throw new Exception("Class does not exist!");

                existingClass = _mapper.Map(request, existingClass);
                existingClass.ModifiedDate = DateTime.Now;
                existingClass.CreatedBy = username;

                var currentFiles = await _fileclassRepository.GetByClassIdAsync(id);
                var filesToDelete = currentFiles
                    .Where(f => request.OldFileIds == null || !request.OldFileIds.Contains(f.Id))
                    .ToList();

                if (filesToDelete.Any())
                {
                    await _fileclassRepository.DeleteByIdsAsync(filesToDelete.Select(f => f.Id).ToList());
                }

                if (request.Attachments != null && request.Attachments.Any())
                {
                    var fileClass = new List<FileClass>();
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

                        fileClass.Add(new FileClass
                        {
                            Id = Guid.NewGuid().ToString(),
                            FileName = fileName,
                            Path = filePath,
                            ClassId = existingClass.Id
                        });
                    }

                    await _fileclassRepository.SaveAllAsync(fileClass);
                }

                var currentEmployeeIds = (await _detailRepository.GetByClassIdAsync(existingClass.Id))
                    .Select(d => d.EmpId)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                var newEmployeeIds = request.EmployeeIds?.Distinct().OrderBy(x => x).ToList() ?? new List<long>();

                var isSame = currentEmployeeIds.SequenceEqual(newEmployeeIds);

                if (!isSame)
                {
                    await _detailRepository.DeleteByClassIdAsync(existingClass.Id);

                    if (newEmployeeIds.Any())
                    {
                        var details = newEmployeeIds.Select(empId => new Detail
                        {
                            ClassId = existingClass.Id,
                            EmpId = empId,
                            SoTinhChi = request.SoTinhChi
                        }).ToList();

                        await _detailRepository.SaveAllAsync(details);
                    }
                }

                var updatedClass = await _classRepository.UpdateAsync(existingClass);

                await _transactionManager.CommitAsync();

                return _mapper.Map<ClassRes>(updatedClass);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error updating course: " + ex.Message, ex);
            }
        }
    }
}
