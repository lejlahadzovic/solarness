using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solarness.Model;
using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Database;
using Solarness.Services.Interface;
using StudentOglasi.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Solarness.Services.Service
{
    public class HomeownerService : BaseCRUDService<Model.Homeowner, Database.Homeowner, HomeownerSearchObject, HomeownerInsertRequest, HomeownerUpdateRequest>, IHomeownerService
    {
        public HomeownerService(SolarnessDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
