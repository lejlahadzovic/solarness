using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Solarness;
using Solarness.Services.Database;
using Solarness.Services.Interface;
using Solarness.Services.Service;
using Solarness.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IDocumentationService, DocumentationService>();
builder.Services.AddTransient<IHomeownerService, HomeownerService>();
builder.Services.AddTransient<IInstallationDetailService, InstallationDetailService>();
builder.Services.AddTransient<IInstallationLocationService, InstallationLocationService>();
builder.Services.AddTransient<ITeamMemberService, TeamMemberService>();
builder.Services.AddTransient<IPermitService, PermitService>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<INotificationService, ObavijestiService>();
builder.Services.AddTransient<ObavijestiService>();
builder.Services.AddTransient<IProjectStatusService, ProjectStatusService>();
builder.Services.AddTransient<ISolarPanelService, SolarPanelService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basicAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "basic"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {      new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id="basicAuth"}
                },
                new string[]{}
        }
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SolarnessDbContext>(options =>
options.UseSqlServer(connectionString));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
