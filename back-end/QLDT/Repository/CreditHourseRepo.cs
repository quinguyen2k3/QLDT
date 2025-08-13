using QLDT.Models;

namespace QLDT.Repository
{
    public interface CreditHourseRepo
    {
        Task<IEnumerable<CreditHourse>> GetAllAsync();
        Task<IEnumerable<CreditHourse>> GetAllIsActiveAsync();
        Task<CreditHourse> CreateAsync(CreditHourse entity);
        Task<CreditHourse?> GetByIdAsync(long id);
        Task<CreditHourse> UpdateAsync(CreditHourse entity);
    }
}
