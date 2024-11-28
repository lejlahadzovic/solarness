using Microsoft.AspNetCore.Mvc;
using Solarness.Controllers;
using Solarness.Model;
using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Interface;
using Solarness.Services.Interfaces;

namespace Solarness.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentationController : BaseCRUDController<Documentation, DocumentationSearchObject, DocumentationInsertRequest, DocumentationUpdateRequest>
    {
        public DocumentationController(ILogger<BaseController<Documentation, DocumentationSearchObject>> logger, IDocumentationService userService) : base(logger, userService)
        {
        }
    }
}
