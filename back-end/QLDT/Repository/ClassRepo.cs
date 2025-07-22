using QLDT.Models;

namespace QLDT.Repository
{
    public interface ClassRepo
    {
        Task<IEnumerable<Class>> GetAllByTrainingFormatIdAsync(long id);
        Task<IEnumerable<Class>> GetAllByTrainingFormatIdAndUsernameAsync(long id,  string username);
        Task<Class> SaveAsync(Class e);
        Task<Class?> GetByIdAsync(long id);
        Task<Class> UpdateAsync(Class e);
    }
}
