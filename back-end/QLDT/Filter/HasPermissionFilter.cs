using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace QLDT.Filter
{
    public class HasPermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly ILogger<HasPermissionFilter> _logger;

        public HasPermissionFilter(string permission, ILogger<HasPermissionFilter> logger = null)
        {
            _permission = permission;
            _logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new JsonResult(new
                {
                    message = "User is not authenticated.",
                    reason = "AuthenticationRequired"
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            var permissionClaims = user.FindAll("permission").Select(c => c.Value).ToList();

            _logger?.LogDebug("Permission claims: [{Claims}]", string.Join(", ", permissionClaims));
            _logger?.LogDebug("Checking for permission: {Permission}", _permission);

            if (!permissionClaims.Any())
            {
                context.Result = new JsonResult(new
                {
                    message = "No permissions found in token.",
                    reason = "PermissionDenied"
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }

            bool hasPermission = permissionClaims.Any(claim =>
            {
                if (claim.StartsWith("[") && claim.EndsWith("]"))
                {
                    try
                    {
                        var permissions = JsonSerializer.Deserialize<string[]>(claim);
                        _logger?.LogDebug("Parsed permissions from claim '{Claim}': [{Permissions}]", claim, string.Join(", ", permissions));
                        return permissions.Any(p => p == _permission);
                    }
                    catch (JsonException)
                    {
                        _logger?.LogWarning("Failed to parse claim '{Claim}' as JSON array", claim);
                    }
                }

                _logger?.LogDebug("Treating claim '{Claim}' as single permission", claim);
                return claim == _permission;
            });

            if (!hasPermission)
            {
                context.Result = new JsonResult(new
                {
                    message = "You do not have permission to access this function.",
                    reason = "PermissionDenied"
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
}