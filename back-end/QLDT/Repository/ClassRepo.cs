using QLDT.Models;

namespace QLDT.Repository
{
    public interface ClassRepo
    {
        Task<IEnumerable<Class>> GetAllByTrainingFormatIdAsync(long id);
        Task<Class> SaveAsync(Class e);
        Task<Class?> GetByIdAsync(long id);
        Task<Class> UpdateAsync(Class e);
    }
}
