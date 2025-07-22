using QLDT.Models;

namespace QLDT.Repository
{
    public interface DetailRepo
    {
        Task SaveAllAsync(IEnumerable<Detail> Detail);
        Task DeleteByClassIdAsync(long classId);
        Task<IEnumerable<Detail>> GetByClassIdAsync(long id);
        Task<IEnumerable<Detail>> GetByEmployeeIdAsync(long id);
    }
}
