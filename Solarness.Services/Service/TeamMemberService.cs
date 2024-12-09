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
    public class TeamMemberService : BaseCRUDService<Model.TeamMember, Database.TeamMember, TeamMemberSearchObject, TeamMemberInsertRequest, TeamMemberUpdateRequest>, ITeamMemberService
    {
        public TeamMemberService(SolarnessDbContext context, IMapper mapper):base(context,mapper)
        {
        }
        public override IQueryable<Database.TeamMember> AddInclude(IQueryable<Database.TeamMember> query, TeamMemberSearchObject? search = null)
        {
            query = query.Include(p => p.User).Include(p => p.Team).AsQueryable();

            return base.AddInclude(query, search);
        }
    }
}