using System.Security.Claims;
using QLDT.Repository;

namespace QLDT.Middlewares
{
    public class CheckUserActiveMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckUserActiveMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserRepo userRepo)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst("id");

                if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
                {
                    var user = await userRepo.GetByIdAsync(userId);
                    Console.WriteLine($"CheckUserActiveMiddleware - userId: {userId}, isActive: {user?.IsActive}");

                    if (user == null || user.IsActive == false)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var errorResponse = new
                        {
                            message = "User is not active",
                            reason = "UserNotActive"
                        };

                        await context.Response.WriteAsJsonAsync(errorResponse);
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
