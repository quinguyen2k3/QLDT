using QLDT.Models;

namespace QLDT.Repository
{
    public interface CourseRepo
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> SaveAsync(Course e);
        Task<Course?> GetByIdAsync(long id);
        Task<Course> UpdateAsync(Course e);
    }
}
