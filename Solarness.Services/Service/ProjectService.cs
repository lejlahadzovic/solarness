using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Solarness.Model;
using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Database;
using Solarness.Services.Interface;
using Solarness.Services.Services;
using Sprache;
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


        public readonly ObavijestiService _obavijestiService;
        public ProjectService(SolarnessDbContext context, IMapper mapper, ObavijestiService obavijestiService) :base(context,mapper)
        {
            _obavijestiService = obavijestiService;
        }

        public override IQueryable<Database.Project> AddInclude(IQueryable<Database.Project> query, ProjectSearchObject? search = null)
        {
            query = query.Include(p => p.Status).Include(p => p.Team).AsQueryable();
            
            return base.AddInclude(query, search);
        }

        public override async Task<Model.Project> Insert(ProjectInsertRequest insert)
        {
            var set = _context.Projects;
            var entity = _mapper.Map<Database.Project>(insert);
            set.Add(entity);
            await _context.SaveChangesAsync();
            await _obavijestiService.SendNotificationProject("Projects", "New project added", entity.ProjectId, "success");
            return _mapper.Map<Model.Project>(entity);
        }
    }
}
