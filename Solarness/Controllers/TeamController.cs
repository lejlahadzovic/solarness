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
    public class TeamController : BaseController<Model.Team, TeamSearchObject>
    {
        public TeamController(ILogger<BaseController<Model.Team, TeamSearchObject>> logger, ITeamService teamService) : base(logger, teamService)
        {
        }
    }
}
