using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using QLDT.Repository;
using System.Threading.Tasks;
using System.Linq;

namespace QLDT.Filter
{
    public class HasPermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly PermissionRepo _repository;

        public HasPermissionFilter(string permission, PermissionRepo repository)
        {
            _permission = permission;
            _repository = repository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            var role = user.FindFirst(ClaimTypes.Role)?.Value
                       ?? user.FindFirst("role")?.Value;

            if (string.IsNullOrEmpty(role))
            {
                context.Result = new ForbidResult();
                return;
            }

            var permissions = await _repository.GetAllByRolenameAsync(role);


            if (!permissions.Any(p => p.Name.Equals(_permission, StringComparison.OrdinalIgnoreCase)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
