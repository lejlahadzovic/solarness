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

        [HttpPost("predict-energy")]
        public IActionResult PredictEnergy()
        {
            try
            {
                var service = new EnergyPredictionService();
                string result = service.RunPythonScript();

                return Ok(new { message = "Prediction completed", output = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
