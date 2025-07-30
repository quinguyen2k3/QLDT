using QLDT.Models;

namespace QLDT.Repository
{
    public interface MajorRepo
    {
        Task<IEnumerable<Major>> GetAllAsync();
        Task<IEnumerable<Major>> GetAllIsActiveAsync();
        Task<Major> CreateAsync(Major e);
        Task<Major?> GetByIdAsync(long id);
        Task<Major> UpdateAsync(Major e);
    }
}
