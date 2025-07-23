using Microsoft.AspNetCore.Mvc;
using QLDT.Filter;

namespace QLDT.Attribute
{
    public class HasPermissionAttribute : TypeFilterAttribute
    {
        public HasPermissionAttribute(string permission)
            : base(typeof(HasPermissionFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}
