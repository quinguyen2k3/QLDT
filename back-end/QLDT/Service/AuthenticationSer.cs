using QLDT.Dtos;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Models;

namespace QLDT.Service
{
    public interface AuthenticationSer
    {
        Task<AuthenticationRes> AuthenticateAsync(AuthenticationReq request);
        Task LogoutAsync(TokenDto request);
        Task ChangePasswordAsync(ChangePasswordReq request);

    }
}
