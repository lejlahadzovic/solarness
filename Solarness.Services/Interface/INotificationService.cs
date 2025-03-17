using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarness.Services.Interface
{
    public interface INotificationService : IService<Model.Notification, BaseSearchObject>
    {
    }
}