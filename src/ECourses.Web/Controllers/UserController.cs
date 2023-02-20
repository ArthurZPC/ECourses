using ECourses.ApplicationCore.Common.Interfaces.Services.Identity;
using ECourses.Web.Attributes;
using ECourses.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECourses.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesErrorResponseType(typeof(ErrorDetails))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Roles/{role}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllUsersInRole(string role = "Administrator")
        {
           var users = await _userService.GetAllUsersInRole(role);
           return Ok(users);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userService.Login(email, password);
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            await _userService.Register(email, password, "User");

            return CreatedAtAction(nameof(Register), null);
        }

        [HttpPost("RegisterInRole")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RegisterInRole(string email, string password, string role)
        {
            await _userService.Register(email, password, role);

            return CreatedAtAction(nameof(RegisterInRole), null);
        }

        [HttpPost(nameof(CreateRole))]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            await _userService.Create(roleName);

            return CreatedAtAction(nameof(CreateRole), null);
        }
    }
}
