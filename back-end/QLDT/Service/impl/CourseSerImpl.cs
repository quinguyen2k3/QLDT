using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Models;
using QLDT.Repository;
using QLDT.Config;

namespace QLDT.Service.impl
{
    public class CourseSerImpl : CourseSer
    {
        private readonly CourseRepo _courseRepository;
        private readonly FileCourseRepo _filecourseRepository;
        private readonly IMapper _mapper;
        private readonly FileConfig _fileConfig;
        private readonly TransactionManager _transactionManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CourseSerImpl(CourseRepo courseRepository,
                     FileCourseRepo filecourseRepository,
                     IMapper mapper,
                     TransactionManager transactionManager,
                     FileConfig fileConfig,
                     IHttpContextAccessor httpContextAccessor)
        {
            _courseRepository = courseRepository;
            _filecourseRepository = filecourseRepository;
            _mapper = mapper;
            _transactionManager = transactionManager;
            _fileConfig = fileConfig;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<CourseRes>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();

            var courseResList = _mapper.Map<IEnumerable<CourseRes>>(courses);

            return courseResList;
        }

        public async Task<CourseRes> CreateAsync(CourseReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
               
                var course = _mapper.Map<Course>(request);

                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                course.CreatedBy = username;
                course.ModifiedDate = course.CreatedDate;
                course.ModifiedBy = course.CreatedBy;

                var createdCourse = await _courseRepository.SaveAsync(course);

                if (request.Attachments != null && request.Attachments.Any())
                {
                    var fileCourses = new List<FileCourse>();

                    var uploadFolder = _fileConfig.UploadBasePath;

                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    foreach (var file in request.Attachments)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine( _fileConfig.UploadBasePath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        fileCourses.Add(new FileCourse
                        {
                            Id = Guid.NewGuid().ToString(),
                            FileName = fileName,
                            Path = filePath,
                            CourseId = createdCourse.Id
                        });
                    }

                    await _filecourseRepository.SaveAllAsync(fileCourses);
                    createdCourse.FileCourses = fileCourses;
                }

                await _transactionManager.CommitAsync();

                var courseRes = _mapper.Map<CourseRes>(createdCourse);
                return courseRes;
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error creating course: " + ex.Message, ex);
            }
        }

        public async Task<CourseRes?> GetByIdAsync(long id)
        {
            var entity = await _courseRepository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<CourseRes>(entity);
        }

        public async Task<CourseRes> UpdateAsync(long id, CourseReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var existingCourse = await _courseRepository.GetByIdAsync(id);
                if (existingCourse == null)
                    throw new Exception("Course does not exist!");

                existingCourse = _mapper.Map(request, existingCourse);
                existingCourse.ModifiedDate = DateTime.Now;
                existingCourse.CreatedBy = username;

                var currentFiles = await _filecourseRepository.GetByCourseIdAsync(id);
                var filesToDelete = currentFiles
                    .Where(f => request.OldFileIds == null || !request.OldFileIds.Contains(f.Id))
                    .ToList();

                if (filesToDelete.Any())
                {
                    await _filecourseRepository.DeleteByIdsAsync(filesToDelete.Select(f => f.Id).ToList());
                }

                if (request.Attachments != null && request.Attachments.Any())
                {
                    var fileCourses = new List<FileCourse>();
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

                        fileCourses.Add(new FileCourse
                        {
                            Id = Guid.NewGuid().ToString(),
                            FileName = fileName,
                            Path = filePath,
                            CourseId = existingCourse.Id
                        });
                    }

                    await _filecourseRepository.SaveAllAsync(fileCourses);
                }

                var updatedCourse = await _courseRepository.UpdateAsync(existingCourse);

                await _transactionManager.CommitAsync();
                return _mapper.Map<CourseRes>(updatedCourse);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error updating course: " + ex.Message, ex);
            }
        }
    }
}
