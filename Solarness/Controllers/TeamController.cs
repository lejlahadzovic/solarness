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
        [HttpGet("{teamId}/members")]
        public async Task<IActionResult> GetTeamMembers(int teamId)
        {
            var members = await (_service as ITeamService).GetTeamMembers(teamId); // Await the async call

            if (members == null || !members.Any()) // Now Any() works correctly
            {
                return NotFound(new { message = "No members found for this team." });
            }

            return Ok(members);
        }
    }
}
