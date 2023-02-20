using ECourses.ApplicationCore.Common.Configuration;
using ECourses.ApplicationCore.Common.Interfaces.Services.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECourses.Web.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtOptions _jwtOptions;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtOptions> jwtOptions)
        {
            _next = next;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await AttachUserToContext(context, userService, token);
            }

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var TokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);

                TokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);

                var userInfo = await userService.GetUserById(userId);

                if (userInfo is not null) 
                {
                    var userRolesClaim = userInfo.Roles.Any() ? new Claim(ClaimTypes.Role, string.Join(", ", userInfo.Roles.Select(r => r.Name))) : null;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, $"User: {token}"),
                        new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                        new Claim(ClaimTypes.Email, userInfo.Email)
                    };

                    if (userRolesClaim is not null)
                    {
                        claims.Add(userRolesClaim);
                    }

                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));

                    context.User = user;
                }
            }
            catch
            {

            }
        }
    }
}
