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

            if (user.Identity!.Name == null)
            {
                var errorDetails = new ErrorDetails
                {
                    ErrorMessage = "Unauthorized"
                };

                context.Result = new JsonResult(errorDetails.ToString())
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }


            if (Roles != null)
            {
                string[] roles = Roles.Trim().Split(",");
                if (!roles.Contains(user.FindFirst(ClaimTypes.Role)!.Value))
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
