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
    public class TeamMemberController : BaseCRUDController<Model.TeamMember, TeamMemberSearchObject, TeamMemberInsertRequest, TeamMemberUpdateRequest>
    {
        public TeamMemberController(ILogger<BaseCRUDController<Model.TeamMember, TeamMemberSearchObject, TeamMemberInsertRequest, TeamMemberUpdateRequest>> logger, ITeamMemberService teamMemberService) : base(logger, teamMemberService)
        {
        }
    }
}
