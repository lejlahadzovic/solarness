using Microsoft.AspNetCore.Mvc;
using Solarness.Controllers;
using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Interface;
using Solarness.Services.Interfaces;

namespace Solarness.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : BaseCRUDController<Model.Project, ProjectSearchObject, ProjectInsertRequest, ProjectUpdateRequest>
    {
        public ProjectController(ILogger<BaseCRUDController<Model.Project, ProjectSearchObject, ProjectInsertRequest, ProjectUpdateRequest>> logger, IProjectService projectService) : base(logger, projectService)
        {
        }
    }
}
