using Microsoft.IdentityModel.Tokens;
using QLDT.Dtos;
using QLDT.Models;
using QLDT.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QLDT.Manager
{
    public class JwtManager
    {
        private readonly IConfiguration _configuration;
        private readonly RefreshTokenRepo _refreshtokenRepo;
        private readonly InvalidTokenRepo _invalidTokenRepo;
        private readonly UserRepo _userRepo;

        public JwtManager(IConfiguration configuration, RefreshTokenRepo refreshtokenRepo, UserRepo userRepo, InvalidTokenRepo invalidTokenRepo)
        {
            _configuration = configuration;
            _refreshtokenRepo = refreshtokenRepo;
            _userRepo = userRepo;
            _invalidTokenRepo = invalidTokenRepo;
        }

        public async Task<TokenDto> GenerateAccessTokenAsync(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSec");
            var secretKey = jwtSettings["Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Username),
                new Claim("name", user.Name ?? ""),
                new Claim("role", user.Role?.Name ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expiresInMinutes = jwtSettings.GetValue<int>("ExpiresInMinutes");

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                JwtId = token.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(3),
                UserId = user.Id
            };

            await _refreshtokenRepo.CreateAsync(refreshTokenEntity);

            return new TokenDto
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public bool Validator(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSec");
            var secretKey = jwtSettings["Key"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validate đầy đủ, nếu còn hạn sẽ throw
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true, // kiểm tra hết hạn
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                // Nếu không throw, tức token còn hạn -> không được cấp lại
                return false;
            }
            catch (SecurityTokenExpiredException)
            {
                try
                {
                    // Bỏ kiểm tra hết hạn, chỉ check chữ ký, issuer, audience
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken _);

                    // Token hết hạn nhưng hợp lệ -> được cấp lại
                    return true;
                }
                catch
                {
                    // Token sai chữ ký, issuer, audience
                    return false;
                }
            }
            catch
            {
                // Token không hợp lệ định dạng hoặc sai chữ ký
                return false;
            }
        }

        public async Task<TokenDto> RenewAccessTokenAsync(TokenDto request)
        {

            bool canRefresh = Validator(request.accessToken);
            if (!canRefresh)
                throw new Exception("Access token is invalid or not expired, cannot refresh.");


            var principal = GetClaims(request.accessToken, validateLifetime: false);
            if (principal == null)
                throw new Exception("Access token is invalid, cannot extract claims.");

            var jti = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var userIdStr = principal.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(jti) || string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                throw new Exception("Access token claims are invalid.");

            var refreshTokenEntity = await _refreshtokenRepo.GetByTokenAsync(request.refreshToken);
            if (refreshTokenEntity == null ||
                refreshTokenEntity.IsUsed ||
                refreshTokenEntity.IsRevoked ||
                refreshTokenEntity.ExpiredAt < DateTime.UtcNow ||
                refreshTokenEntity.JwtId != jti)
            {
                throw new Exception("Invalid refresh token.");
            }

            refreshTokenEntity.IsUsed = true;
            await _refreshtokenRepo.UpdateAsync(refreshTokenEntity);

            var user = await _userRepo.GetByIdAsync(userId);

            var newToken = await GenerateAccessTokenAsync(user);

            return newToken;
        }
        public ClaimsPrincipal? GetClaims(string accessToken, bool validateLifetime = true)
        {
            var jwtSettings = _configuration.GetSection("JwtSec");
            var secretKey = jwtSettings["Key"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = validateLifetime,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public async Task RevokeTokenAsync(TokenDto request)
        {
            var refreshTokenEntity = await _refreshtokenRepo.GetByTokenAsync(request.refreshToken);
            if (refreshTokenEntity == null)
                throw new Exception("Refresh token does not exist.");
            if (refreshTokenEntity.IsRevoked)
                throw new Exception("Refresh token has already been revoked.");

            refreshTokenEntity.IsRevoked = true;

            await _refreshtokenRepo.UpdateAsync(refreshTokenEntity);

            var principal = GetClaims(request.accessToken, validateLifetime: false);
            if (principal == null)
                throw new Exception("Invalid access token.");

            var username = principal.FindFirst("username")?.Value ?? "unknown";

            var jti = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var expUnix = principal.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

            if (string.IsNullOrEmpty(jti) || string.IsNullOrEmpty(expUnix))
                throw new Exception("Access token missing required claims.");

            if (!long.TryParse(expUnix, out long expSeconds))
                throw new Exception("Invalid expiration claim in token.");

            var expiration = DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime;

            var invalidToken = new InvalidToken
            {
                Jti = jti,
                Expiration = expiration,
                RevokedAt = DateTime.UtcNow,
                RevokedBy = username
            };

            await _invalidTokenRepo.CreateAsync(invalidToken);
        }
    }
}