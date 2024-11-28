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
    public class InstallationDetailController : BaseCRUDController<Model.InstallationDetail, InstallationDetailSearchObject, InstallationDetailInsertRequest, InstallationDetailUpdateRequest>
    {
        public InstallationDetailController(ILogger<BaseController<Model.InstallationDetail, InstallationDetailSearchObject>> logger, IInstallationDetailService installationDetailService) : base(logger, installationDetailService)
        {
        }
    }
}
