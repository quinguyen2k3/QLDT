using QLDT.Repository;
using System.IdentityModel.Tokens.Jwt;

namespace QLDT.Middlewares
{
    public class InvalidTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<InvalidTokenMiddleware> _logger;

        public InvalidTokenMiddleware(RequestDelegate next, ILogger<InvalidTokenMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, InvalidTokenRepo invalidTokenRepo)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var accessToken = authHeader.Substring("Bearer ".Length).Trim();

                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(accessToken);

                    var jti = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

                    if (!string.IsNullOrEmpty(jti))
                    {
                        var isRevoked = await invalidTokenRepo.ExistsAsync(jti);
                        if (isRevoked)
                        {
                            _logger.LogWarning($"Access token with JTI {jti} has been revoked. Returning 404.");

                            context.Response.StatusCode = StatusCodes.Status404NotFound;
                            await context.Response.WriteAsync("Access token has been revoked or expired.");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error parsing JWT in InvalidTokenMiddleware.");
                    // Cho qua Authentication middleware xử lý nếu JWT lỗi
                }
            }

            await _next(context);
        }
    }
}
