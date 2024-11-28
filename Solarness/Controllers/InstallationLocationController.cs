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
    public class InstallationLocationController : BaseCRUDController<Model.InstallationLocation, InstallationLocationSearchObject, InstallationLocationInsertRequest, InstallationLocationUpdateRequest>
    {
        public InstallationLocationController(ILogger<BaseCRUDController<Model.InstallationLocation, InstallationLocationSearchObject, InstallationLocationInsertRequest, InstallationLocationUpdateRequest>> logger, IInstallationLocationService installationLocationService) : base(logger, installationLocationService)
        {
        }
    }
}
