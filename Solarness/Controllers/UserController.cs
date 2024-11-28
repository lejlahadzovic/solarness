using Microsoft.AspNetCore.Mvc;
using Solarness.Controllers;
using Solarness.Model;
using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Interface;
using Solarness.Services.Interfaces;

namespace Solarness.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseCRUDController<User, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        public UserController(ILogger<BaseController<User, UserSearchObject>> logger, IUserService userService) : base(logger, userService)
        {
        }
    }
}
