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
    public class NotificationController : BaseController<Model.Notification, BaseSearchObject>
    {
        public NotificationController(ILogger<BaseController<Model.Notification, BaseSearchObject>> logger, INotificationService notificationService) : base(logger, notificationService)
        {
        }
    }
}
