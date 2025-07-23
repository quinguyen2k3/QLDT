using QLDT.Models;

namespace QLDT.Repository
{
    public interface MajorRepo
    {
        Task<IEnumerable<Major>> GetAllAsync();
        Task<IEnumerable<Major>> GetAllIsActiveAsync();
        Task<IEnumerable<Major>> GetAllByUsernameAsync(string username);
        Task<Major> CreateAsync(Major e);
        Task<Major?> GetByIdAsync(long id);
        Task<Major> UpdateAsync(Major e);
    }
}
