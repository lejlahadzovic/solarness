using Microsoft.AspNetCore.Mvc;
using Solarness.Controllers;
using Solarness.Model;
using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Interface;
using Solarness.Services.Interfaces;
using System.Security.Claims;

namespace Solarness.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseCRUDController<User, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        private readonly IUserService _userService;
        public UserController(ILogger<BaseController<User, UserSearchObject>> logger, IUserService userService) : base(logger, userService)
        {
            _userService = userService;
        }

        [HttpGet("currentUser")]
        public async Task<IActionResult> GetLoggedInUser()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
