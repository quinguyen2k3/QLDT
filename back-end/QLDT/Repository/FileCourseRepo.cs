using QLDT.Models;

namespace QLDT.Repository
{
    public interface FileCourseRepo
    {
        Task<IEnumerable<FileCourse>> SaveAllAsync(IEnumerable<FileCourse> fileCourses);
        Task DeleteByIdsAsync(List<string> ids);
        Task<IEnumerable<FileCourse>> GetByCourseIdAsync(long id);
    }
}
