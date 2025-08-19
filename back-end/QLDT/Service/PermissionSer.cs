using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface PermissionSer
    {
        Task<IEnumerable<PermissionRes>> GetAllByUserAsync();
    }
}
