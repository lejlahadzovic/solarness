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
    public class ProjectService : BaseCRUDService<Model.Project, Database.Project, ProjectSearchObject, ProjectInsertRequest, ProjectUpdateRequest>, IProjectService
    {
        public ProjectService(SolarnessDbContext context, IMapper mapper):base(context,mapper)
        {
        }

        public override IQueryable<Database.Project> AddInclude(IQueryable<Database.Project> query, ProjectSearchObject? search = null)
        {
            query = query.Include(p => p.Status).Include(p => p.Team).AsQueryable();
            
            return base.AddInclude(query, search);
        }
    }
}
