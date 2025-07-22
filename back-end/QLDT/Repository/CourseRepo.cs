using QLDT.Models;

namespace QLDT.Repository
{
    public interface CourseRepo
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<IEnumerable<Course>> GetAllIsActiveAsync();
        Task<IEnumerable<Course>> GetAllByUsernameAsync(string username);
        Task<Course> SaveAsync(Course e);
        Task<Course?> GetByIdAsync(long id);
        Task<Course> UpdateAsync(Course e);
    }
}
