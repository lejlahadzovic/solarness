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
    public class PermitController : BaseCRUDController<Model.Permit, PermitSearchObject, PermitInsertRequest, PermitUpdateRequest>
    {
        public PermitController(ILogger<BaseCRUDController<Model.Permit, PermitSearchObject, PermitInsertRequest, PermitUpdateRequest>> logger, IPermitService permitService) : base(logger, permitService)
        {
        }
    }
}
