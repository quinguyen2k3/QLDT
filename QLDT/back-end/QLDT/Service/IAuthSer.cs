using QLDT.Dtos.Request;
using QLDT.Dtos.Response;

namespace QLDT.Service
{
    public interface IAuthSer
    {
        Task<LoginRes> LoginAsync(LoginReq req);
        Task LogoutAsync();  // với JWT thì thường chỉ client-side
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordReq req);
    }
}
