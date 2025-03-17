using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solarness.Model;
using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Database;
using Solarness.Services.Interface;
using Sprache;
using StudentOglasi.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Solarness.Services.Service
{
    public class TeamService : BaseService<Model.Team, Database.Team, TeamSearchObject>, ITeamService
    {
        public TeamService(SolarnessDbContext context, IMapper mapper):base(context,mapper)
        {
        }

        public async Task<List<Model.TeamMember>> GetTeamMembers(int teamId)
        {
            var list = await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId)
                .Include(tm => tm.Team)  // Include team details
                .Include(tm => tm.User)
                .Include(tm => tm.User.Role)// Include user details
                .ToListAsync();          // Fetch full TeamMember objects

            var tmp = _mapper.Map<List<Model.TeamMember>>(list); // Map full TeamMember model

            return tmp;
        }

       
    }
}
