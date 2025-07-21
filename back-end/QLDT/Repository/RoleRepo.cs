using QLDT.Models;

namespace QLDT.Repository
{
    public interface RoleRepo
    {
        Task<IEnumerable<Role>> GetAllAsync();
    }
}
