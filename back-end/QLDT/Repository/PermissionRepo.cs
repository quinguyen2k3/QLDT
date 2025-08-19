using QLDT.Models;

namespace QLDT.Repository
{
    public interface PermissionRepo
    {
        Task<IEnumerable<Permission>> GetAllByRolenameAsync(string name);
        Task<IEnumerable<Permission>> GetAllByUserIdAsync(long id);
    }
}
