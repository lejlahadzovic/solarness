using Microsoft.AspNetCore.Mvc;
using Solarness.Controllers;
using Solarness.Model;
using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Interface;
using Solarness.Services.Interfaces;
using Solarness.Services.Service;

namespace Solarness.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeownerController : BaseCRUDController<Homeowner, HomeownerSearchObject, HomeownerInsertRequest, HomeownerUpdateRequest>
    {
        public HomeownerController(ILogger<BaseController<Homeowner, HomeownerSearchObject>> logger, IHomeownerService homeownerService) : base(logger, homeownerService)
        {
        }
    }
}