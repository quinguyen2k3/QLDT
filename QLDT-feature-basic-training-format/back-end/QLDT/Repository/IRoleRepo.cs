using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Models;

namespace QLDT.Repository
{
    public interface IRoleRepo
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(long id);
        Task<Role> CreateAsync(Role entity);
        Task<Role> UpdateAsync(Role entity);
        Task DeleteAsync(Role entity);
        Task SaveChangesAsync();
    }
}
