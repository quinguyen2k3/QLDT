using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface CourseSer
    {
        Task<IEnumerable<CourseRes>> GetAllAsync();
        Task<IEnumerable<CourseRes>> GetAllActiveAsync();
        Task<IEnumerable<CourseRes>> GetAllByUserAsync();
        Task<CourseRes> CreateAsync(CourseReq request);
        Task<CourseRes?> GetByIdAsync(long id);
        Task<CourseRes> UpdateAsync(long id, CourseReq request);
    }
}
