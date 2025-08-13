using QLDT.Models;

namespace QLDT.Repository
{
    public interface ClassRepo
    {
        Task<IEnumerable<Class>> GetAllAsync();
        Task<IEnumerable<Class>> GetAllByTrainingFormatIdAsync(long id);
        Task<IEnumerable<Class>> GetAllByUsernameAsync(string username);
        Task<Class> SaveAsync(Class e);
        Task<Class?> GetByIdAsync(long id);
        Task<Class> UpdateAsync(Class e);
    }
}
