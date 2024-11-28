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
    public class ProjectStatusController : BaseController<Model.ProjectStatus, ProjectStatusSearchObject>
    {
        public ProjectStatusController(ILogger<BaseController<Model.ProjectStatus, ProjectStatusSearchObject>> logger, IProjectStatusService projectStatusService) : base(logger, projectStatusService)
        {
        }
    }
}
