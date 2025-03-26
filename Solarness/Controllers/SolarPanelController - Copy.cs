using Microsoft.AspNetCore.Mvc;
using Solarness.Controllers;
using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Interface;
using Solarness.Services.Interfaces;
using Solarness.Services.Service;

namespace Solarness.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : BaseController<Model.Role, BaseSearchObject>
    {
        public RoleController(ILogger<BaseController<Model.Role, BaseSearchObject>> logger, IRoleService roleService) : base(logger, roleService)
        {
        }
    }
}
