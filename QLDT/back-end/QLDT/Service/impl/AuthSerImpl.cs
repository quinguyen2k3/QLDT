using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QLDT.Dtos.Request;
using QLDT.Dtos.Response;
using QLDT.Models;
using QLDT.Repository;

namespace QLDT.Service.impl
{
    public class AuthSerImpl : IAuthSer
    {
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _cfg;
        public AuthSerImpl(IUserRepo userRepo, IConfiguration cfg)
        {
            _userRepo = userRepo;
            _cfg = cfg;
        }

        public async Task<LoginRes> LoginAsync(LoginReq req)
        {
            var user = await _userRepo.FindByUsernameAsync(req.Username);
            if (user == null || !AuthHash.Verify(req.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Username or password is incorrect");

            // sinh JWT
            var keyBytes = Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    // nếu có role: new Claim(ClaimTypes.Role, ...)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return new LoginRes
            {
                Token = handler.WriteToken(token),
                Expires = token.ValidTo
            };
        }

        public Task LogoutAsync()
        {
            // Với JWT thường không cần server–side
            return Task.CompletedTask;
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordReq req)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null || !AuthHash.Verify(req.OldPassword, user.PasswordHash))
                return false;

            user.PasswordHash = AuthHash.Hash(req.NewPassword);
            await _userRepo.SaveChangesAsync();
            return true;
        }
    }
}
