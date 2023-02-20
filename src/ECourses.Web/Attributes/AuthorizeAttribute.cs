using ECourses.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ECourses.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter, IAuthorizeData
    {
        public string? Policy { get; set; }
        public string? Roles { get; set; }
        public string? AuthenticationSchemes { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity!.Name is null)
            {
                var errorDetails = new ErrorDetails
                {
                    ErrorMessage = "Unauthorized"
                };

                context.Result = new JsonResult(errorDetails.ToString())
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                };

                return;
            }

            if (Roles is not null)
            {
                var roles = Roles.Trim().Split(",");

                var userRoles = user.FindFirst(ClaimTypes.Role)?.Value;

                var userRolesList = userRoles?.Split(",");

                if (userRolesList is null || !roles.Any(r => userRolesList.Contains(r)))
                {
                    var errorDetails = new ErrorDetails
                    {
                        ErrorMessage = "User role is not permitted."
                    };

                    context.Result = new JsonResult(errorDetails.ToString())
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        ContentType = "application/json",
                    };
                }
            }
        }
    }
}
