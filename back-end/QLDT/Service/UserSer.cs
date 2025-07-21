using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface UserSer
    {
        Task<IEnumerable<UserRes>> GetAllAsync();
        Task<UserRes> CreateAsync(UserReq req);
        Task<UserRes?> GetByIdAsync(long id);
        Task<UserRes?> UpdateAsync(long id, UserReq req);
    }
}
