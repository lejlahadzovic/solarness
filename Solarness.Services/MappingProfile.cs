using AutoMapper;
using Solarness.Model;
using Solarness.Services.Database;

namespace Solarness.Services
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Database.User,Model.User>();
            CreateMap<Model.Requests.UserInsertRequest, Database.User>();
            CreateMap<Model.Requests.UserUpdateRequest, Database.User>(); 
            CreateMap<Database.Permit, Model.Permit>();
            CreateMap<Model.Requests.PermitInsertRequest, Database.Permit>();
            CreateMap<Model.Requests.PermitUpdateRequest, Database.Permit>();
            CreateMap<Database.Project, Model.Project>();
            CreateMap<Model.Requests.ProjectInsertRequest, Database.Project>();
            CreateMap<Model.Requests.ProjectUpdateRequest, Database.Project>();
            CreateMap<Database.Documentation, Model.Documentation>();
            CreateMap<Model.Requests.DocumentationInsertRequest, Database.Documentation>();
            CreateMap<Model.Requests.DocumentationUpdateRequest, Database.Documentation>();
            CreateMap<Database.Homeowner, Model.Homeowner>();
            CreateMap<Model.Requests.HomeownerInsertRequest, Database.Homeowner>();
            CreateMap<Model.Requests.HomeownerUpdateRequest, Database.Homeowner>();
            CreateMap<Database.InstallationLocation, Model.InstallationLocation>();
            CreateMap<Model.Requests.InstallationLocationInsertRequest, Database.InstallationLocation>();
            CreateMap<Model.Requests.InstallationLocationUpdateRequest, Database.InstallationLocation>();
            CreateMap<Database.InstallationDetail, Model.InstallationDetail>();
            CreateMap<Model.Requests.InstallationDetailInsertRequest, Database.InstallationDetail>();
            CreateMap<Model.Requests.InstallationDetailUpdateRequest, Database.InstallationDetail>();
            CreateMap<Database.TeamMember, Model.TeamMember>();
            CreateMap<Model.Requests.TeamMemberInsertRequest, Database.TeamMember>();
            CreateMap<Model.Requests.TeamMemberUpdateRequest, Database.TeamMember>();
            CreateMap<Database.Team, Model.Team>();
            CreateMap<Database.Task, Model.Task>();
            CreateMap<Model.Requests.TaskInsertRequest, Database.Task>();
            CreateMap<Model.Requests.TaskUpdateRequest, Database.Task>();
            CreateMap<Database.SolarPanel, Model.SolarPanel>();
            CreateMap<Database.ProjectStatus, Model.ProjectStatus>();
        }
    }
}