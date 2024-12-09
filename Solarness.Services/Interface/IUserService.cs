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
    public interface IUserService: ICRUDService<Model.User,UserSearchObject,UserInsertRequest,UserUpdateRequest>
    {
        public Task<Model.User> Login(string username, string password);
        public Task<Model.User> GetUserByUsername(string username);
    }
}
