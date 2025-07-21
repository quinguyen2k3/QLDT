using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface RoleSer
    {
        Task<IEnumerable<RoleRes>> GetAllAsync();
    }
}
