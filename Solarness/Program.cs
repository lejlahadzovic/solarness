using Microsoft.EntityFrameworkCore;
using Solarness.Services.Database;
using Solarness.Services.Interface;
using Solarness.Services.Service;

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
builder.Services.AddTransient<IProjectStatusService, ProjectStatusService>();
builder.Services.AddTransient<ISolarPanelService, SolarPanelService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseAuthorization();

app.MapControllers();

app.Run();
