using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QLDT.Cache;
using QLDT.Service;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLDT.Filter
{
    public class HasPermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly PermissionCache _permissionCache;

        public HasPermissionFilter(string permission, PermissionCache permissionCache)
        {
            _permission = permission;
            _permissionCache = permissionCache;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var userIdStr = user.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !long.TryParse(userIdStr, out long userId))
            {
                context.Result = new ForbidResult();
                return;
            }

            var permissions = await _permissionCache.GetPermissionsAsync(userId);
            if (!permissions.Any(p => p.Equals(_permission, StringComparison.OrdinalIgnoreCase)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}