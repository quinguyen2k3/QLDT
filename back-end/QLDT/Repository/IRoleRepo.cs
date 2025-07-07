using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface IRoleRepo
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(long id);
        Task<Role> CreateAsync(Role e);
        Task<Role> UpdateAsync(Role e);
        Task DeleteAsync(Role e);
        Task SaveChangesAsync();
    }
}