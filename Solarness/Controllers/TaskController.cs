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
    public class TaskController : BaseCRUDController<Model.Task, TaskSearchObject, TaskInsertRequest, TaskUpdateRequest>
    {
        public TaskController(ILogger<BaseCRUDController<Model.Task, TaskSearchObject, TaskInsertRequest, TaskUpdateRequest>> logger, ITaskService taskService) : base(logger, taskService)
        {
        }
    }
}
