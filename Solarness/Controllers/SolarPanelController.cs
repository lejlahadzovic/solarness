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
    public class SolarPanelController : BaseController<Model.SolarPanel, SolarPanelSearchObject>
    {
        public SolarPanelController(ILogger<BaseController<Model.SolarPanel, SolarPanelSearchObject>> logger, ISolarPanelService solarPanelService) : base(logger, solarPanelService)
        {
        }
    }
}
