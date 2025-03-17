using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Solarness.Services.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Homeowners",
                columns: table => new
                {
                    HomeownerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Homeowne__8454E15114C8D373", x => x.HomeownerId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectS__C8EE2063A9D2C0C0", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE1AF69E740C", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C477F1C10", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__Users__RoleId__286302EC",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teams__123AE7990851A463", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK__Teams__UserId__2D27B809",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    ProjectDescription = table.Column<string>(type: "text", nullable: true),
                    StreetAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    KW = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ContractAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SiteInspectionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EngineeringSubmitDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EngineeringReceivedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SaleDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Significance = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Urgency = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PriorityLevel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    HomeownerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Projects__761ABEF0EB7D0886", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK__Projects__Homeow__02FC7413",
                        column: x => x.HomeownerId,
                        principalTable: "Homeowners",
                        principalColumn: "HomeownerId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Projects__Status__37A5467C",
                        column: x => x.StatusId,
                        principalTable: "ProjectStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Projects__TeamId__38996AB5",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                    table.ForeignKey(
                        name: "FK__Projects__UserId__398D8EEE",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TeamMemb__0CF04B1815329411", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK__TeamMembe__TeamI__31EC6D26",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                    table.ForeignKey(
                        name: "FK__TeamMembe__UserI__30F848ED",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Documentation",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    DocumentType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DocumentLocation = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    AdditionDate = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__1ABEEF0FCDC0F7AC", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK__Documenta__Proje__7F2BE32F",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Financing",
                columns: table => new
                {
                    FinancingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    FinancingName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FinancingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinancingDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Financin__706F686FB3BA6B0B", x => x.FinancingId);
                    table.ForeignKey(
                        name: "FK__Financing__Proje__44FF419A",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "InstallationLocations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Installa__E7FEA49765C8A2A2", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK__Installat__Proje__628FA481",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    MilestoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    MilestoneName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MilestoneDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mileston__09C48078AD285774", x => x.MilestoneId);
                    table.ForeignKey(
                        name: "FK__Milestone__Proje__3C69FB99",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    SendDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__20CF2E1236416497", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Notificat__Proje__5EBF139D",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Permits",
                columns: table => new
                {
                    PermitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    PermitSubmitDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PermitReceivedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PermitType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LastUpdatedDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permits__0B0E6DD0C6F2C103", x => x.PermitId);
                    table.ForeignKey(
                        name: "FK__Permits__Project__3F466844",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Predictions",
                columns: table => new
                {
                    PredictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    PredictionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PredictionStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Confidence = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Predicti__BAE4C1A05C2174BA", x => x.PredictionId);
                    table.ForeignKey(
                        name: "FK__Predictio__Proje__59063A47",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "ProjectPhases",
                columns: table => new
                {
                    PhaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhaseName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectP__5BA26D62F4DD17A6", x => x.PhaseId);
                    table.ForeignKey(
                        name: "FK__ProjectPh__Proje__4CA06362",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "ProjectStages",
                columns: table => new
                {
                    StageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    StageName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    StageGroup = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    StageStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StageEndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DaysInStage = table.Column<int>(type: "int", nullable: true),
                    StageUpdatedDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectS__03EB7AD812943F9E", x => x.StageId);
                    table.ForeignKey(
                        name: "FK__ProjectSt__Proje__4222D4EF",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "SolarPanels",
                columns: table => new
                {
                    PanelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    SerialNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    InstallationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Efficiency = table.Column<double>(type: "float", nullable: true),
                    EnergyProduced = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SolarPan__49CA680639FACEF4", x => x.PanelId);
                    table.ForeignKey(
                        name: "FK__SolarPane__Proje__5165187F",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    MemberId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tasks__7C6949B19478DBB1", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK__Tasks__MemberId__49C3F6B7",
                        column: x => x.MemberId,
                        principalTable: "TeamMembers",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__Tasks__ProjectId__48CFD27E",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "InstallationDetails",
                columns: table => new
                {
                    InstallationDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    InstallStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    InstallCompleteDate = table.Column<DateOnly>(type: "date", nullable: true),
                    InstallationType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NumberOfPanels = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Installa__E00A47FA8D970B28", x => x.InstallationDetailId);
                    table.ForeignKey(
                        name: "FK__Installat__Locat__797309D9",
                        column: x => x.LocationId,
                        principalTable: "InstallationLocations",
                        principalColumn: "LocationId");
                    table.ForeignKey(
                        name: "FK__Installat__Proje__7B5B524B",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK__Installat__UserI__7A672E12",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceHistory",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PanelId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Efficiency = table.Column<double>(type: "float", nullable: true),
                    EnergyProduced = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Performa__4D7B4ABDE9D0F6E4", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK__Performan__Panel__5535A963",
                        column: x => x.PanelId,
                        principalTable: "SolarPanels",
                        principalColumn: "PanelId");
                });

            migrationBuilder.InsertData(
                table: "Homeowners",
                columns: new[] { "HomeownerId", "Address", "City", "Country", "Email", "FirstName", "LastName", "PhoneNumber", "PostalCode" },
                values: new object[,]
                {
                    { 1, "123 Solar St.", "Solar City", "SolarLand", "johndoe@email.com", "John", "Doe", "123-456-7890", "12345" },
                    { 2, "456 Green Ave.", "Eco Town", "EcoLand", "janesmith@email.com", "Jane", "Smith", "987-654-3210", "67890" },
                    { 3, "789 Windy Rd.", "Wind City", "WindLand", "alicejohnson@email.com", "Alice", "Johnson", "555-123-4567", "11223" },
                    { 4, "789 Windy Rd.", "Wind City", "WindLand", "alicejohnson@email.com", "Sam", "Sam", "555-123-4567", "11223" }
                });

            migrationBuilder.InsertData(
                table: "ProjectStatus",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Planning" },
                    { 2, "In Progress" },
                    { 3, "Completed" },
                    { 4, "On Hold" },
                    { 5, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator with full access to all system functionalities.", "Admin" },
                    { 2, "Responsible for managing projects and overseeing team members.", "Manager" },
                    { 3, "Regular user with access to view and interact with projects.", "User" },
                    { 4, "Responsible for the installation and maintenance of solar panels.", "Technician" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "PhoneNumber", "Picture", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "johndoe@email.com", "John", "Doe", "JfJzsL3ngGWki+Dn67C+8WLy73I=", "7TUJfmgkkDvcY3PB/M4fhg==", "123-456-7890", "path/to/picture.jpg", 1, "johny" },
                    { 2, "janesmith@email.com", "Jane", "Smith", "ug0GgEnT5hKaHsfTn1l1kiGvZAg=", "qh31pfpS2ox1h96QPhmR/Q==", "098-765-4321", "path/to/picture2.jpg", 3, "janes" },
                    { 3, "alices@email.com", "Alice", "Snow", "JfJzsL3ngGWki+Dn67C+8WLy73I=", "7TUJfmgkkDvcY3PB/M4fhg==", "123-456-7890", "path/to/picture.jpg", 2, "alice" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "CreationDate", "Description", "TeamName", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Team responsible for installing solar panels.", "Solar Installers", 1 },
                    { 2, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineers overseeing solar panel projects.", "Project Engineers", 2 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "City", "ContractAmount", "EngineeringReceivedDate", "EngineeringSubmitDate", "HomeownerId", "KW", "PriorityLevel", "ProjectDescription", "ProjectName", "SaleDate", "Significance", "SiteInspectionDate", "StatusId", "StreetAddress", "TeamId", "Urgency", "UserId" },
                values: new object[,]
                {
                    { 1, "Solar City", 25000.00m, new DateOnly(2025, 3, 20), new DateOnly(2025, 3, 15), 1, 15.5m, "High", "This project involves the installation of a solar panel system for a residential home located in the city center.", "Solar Panel Installation for Residential Home", new DateOnly(2025, 2, 5), "High", new DateOnly(2025, 3, 10), 1, "123 Solar St.", 1, "Medium", 1 },
                    { 2, "Solar City", 500000.00m, new DateOnly(2025, 4, 12), new DateOnly(2025, 4, 10), 2, 100.0m, "Medium", "A commercial solar panel installation for a local office building.", "Commercial Solar Panel Project", new DateOnly(2025, 3, 1), "Medium", new DateOnly(2025, 4, 5), 2, "456 Green Ave.", 2, "Low", 2 },
                    { 3, "Solar City", 25000.00m, new DateOnly(2025, 3, 20), new DateOnly(2025, 3, 15), 3, 15.5m, "High", "This project involves the installation of a solar panel system for a residential home located in the city center.", "Solar Panel Installation for Company", new DateOnly(2025, 2, 5), "High", new DateOnly(2025, 3, 10), 1, "123 Solar St.", 1, "Medium", 1 },
                    { 4, "Solar City", 25000.00m, new DateOnly(2025, 3, 20), new DateOnly(2025, 3, 15), 4, 15.5m, "High", "This project involves the installation of a solar panel system for a residential home located in the city center.", "Solar Panel and system Installation ", new DateOnly(2025, 2, 5), "High", new DateOnly(2025, 3, 10), 1, "123 Solar St.", 1, "Medium", 1 }
                });

            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "MemberId", "TeamId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Documentation",
                columns: new[] { "DocumentId", "AdditionDate", "DocumentLocation", "DocumentName", "DocumentType", "ProjectId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 1, 10), "/documents/site_plan_1.pdf", "Site Plan", "PDF", 1 },
                    { 2, new DateOnly(2024, 2, 5), "/documents/permit_approval_1.pdf", "Permit Approval", "PDF", 1 },
                    { 3, new DateOnly(2024, 3, 15), "/documents/electrical_layout_2.dwg", "Electrical Layout", "DWG", 2 },
                    { 4, new DateOnly(2024, 4, 2), "/documents/inspection_report_3.docx", "Inspection Report", "DOCX", 3 }
                });

            migrationBuilder.InsertData(
                table: "Financing",
                columns: new[] { "FinancingId", "FinancingAmount", "FinancingDate", "FinancingName", "ProjectId" },
                values: new object[,]
                {
                    { 1, 50000.00m, new DateOnly(2024, 1, 15), "Government Subsidy", 1 },
                    { 2, 75000.00m, new DateOnly(2024, 2, 20), "Bank Loan", 2 },
                    { 3, 100000.00m, new DateOnly(2024, 3, 5), "Private Investment", 3 },
                    { 4, 25000.00m, new DateOnly(2024, 4, 10), "Crowdfunding", 4 }
                });

            migrationBuilder.InsertData(
                table: "InstallationLocations",
                columns: new[] { "LocationId", "Address", "City", "Country", "Latitude", "LocationName", "Longitude", "ProjectId" },
                values: new object[,]
                {
                    { 1, "123 Solar St, Green Valley", "Green Valley", "USA", 34.052199999999999, "Solar Installation Site 1", -118.2437, 1 },
                    { 2, "456 Sunshine Rd, Tech Park", "Tech City", "USA", 40.712800000000001, "Solar Installation Site 2", -74.006, 2 },
                    { 3, "789 Bright Ave, Oak Town", "Oak Town", "Canada", 43.651699999999998, "Solar Installation Site 3", -79.383200000000002, 3 },
                    { 4, "101 Sunbeam Blvd, River City", "River City", "Canada", 45.421500000000002, "Solar Installation Site 4", -75.699200000000005, 4 }
                });

            migrationBuilder.InsertData(
                table: "Milestones",
                columns: new[] { "MilestoneId", "Description", "MilestoneDate", "MilestoneName", "ProjectId" },
                values: new object[,]
                {
                    { 1, "The project officially kicks off with team and stakeholder introductions.", new DateOnly(2024, 1, 1), "Project Kickoff", 1 },
                    { 2, "Final designs for the solar panel layout are approved.", new DateOnly(2024, 2, 15), "Design Approval", 1 },
                    { 3, "The necessary permits are approved for installation.", new DateOnly(2024, 3, 5), "Permit Approval", 2 },
                    { 4, "All required materials for installation are procured.", new DateOnly(2024, 3, 25), "Material Procurement", 2 },
                    { 5, "The site is prepared for solar panel installation.", new DateOnly(2024, 4, 10), "Site Preparation", 3 }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "NotificationId", "Content", "ProjectId", "SendDate", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Your solar project has been approved and is ready for the next phase.", 1, new DateTime(2024, 3, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Project Approved", null },
                    { 2, "The installation for your project is scheduled for next week.", 2, new DateTime(2024, 3, 5, 14, 30, 0, 0, DateTimeKind.Unspecified), "Installation Scheduled", null },
                    { 3, "Your solar panel installation has been completed. A final inspection is scheduled.", 1, new DateTime(2024, 3, 10, 9, 15, 0, 0, DateTimeKind.Unspecified), "Final Inspection", null }
                });

            migrationBuilder.InsertData(
                table: "Permits",
                columns: new[] { "PermitId", "LastUpdatedDate", "PermitReceivedDate", "PermitSubmitDate", "PermitType", "ProjectId", "Status" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 2, 21), new DateOnly(2024, 2, 20), new DateOnly(2024, 2, 10), "Construction", 1, "Approved" },
                    { 2, new DateOnly(2024, 3, 5), null, new DateOnly(2024, 3, 1), "Environmental", 2, "Pending" },
                    { 3, new DateOnly(2024, 2, 1), new DateOnly(2024, 1, 30), new DateOnly(2024, 1, 15), "Electrical", 3, "Approved" }
                });

            migrationBuilder.InsertData(
                table: "ProjectPhases",
                columns: new[] { "PhaseId", "Description", "EndDate", "PhaseName", "ProjectId", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, "Initial phase for project feasibility and design", new DateOnly(2024, 2, 28), "Planning", 1, new DateOnly(2024, 2, 1), "Completed" },
                    { 2, "Regulatory approvals and paperwork processing", new DateOnly(2024, 3, 20), "Permitting", 1, new DateOnly(2024, 3, 1), "In Progress" },
                    { 3, "Solar panels and system installation on-site", new DateOnly(2024, 4, 10), "Installation", 1, new DateOnly(2024, 3, 22), "Not Started" }
                });

            migrationBuilder.InsertData(
                table: "ProjectStages",
                columns: new[] { "StageId", "DaysInStage", "ProjectId", "StageEndDate", "StageGroup", "StageName", "StageStartDate", "StageUpdatedDate" },
                values: new object[,]
                {
                    { 1, 5, 1, new DateOnly(2024, 3, 5), "Planning", "Initial Site Survey", new DateOnly(2024, 3, 1), new DateOnly(2024, 3, 6) },
                    { 2, 15, 1, new DateOnly(2024, 3, 20), "Regulatory", "Permit Approval", new DateOnly(2024, 3, 6), new DateOnly(2024, 3, 21) },
                    { 3, 10, 1, new DateOnly(2024, 4, 1), "Construction", "Panel Installation", new DateOnly(2024, 3, 22), new DateOnly(2024, 4, 2) }
                });

            migrationBuilder.InsertData(
                table: "SolarPanels",
                columns: new[] { "PanelId", "Efficiency", "EnergyProduced", "InstallationDate", "ModelName", "ProjectId", "SerialNumber" },
                values: new object[,]
                {
                    { 1, 22.699999999999999, 5000.0, new DateOnly(2024, 5, 15), "SunPower X22-370", 1, "SPX22-370-12345" },
                    { 2, 21.399999999999999, 4000.0, new DateOnly(2024, 6, 5), "LG NeON R 370W", 4, "LGNR-370-67890" },
                    { 3, 20.5, 4500.0, new DateOnly(2024, 7, 8), "Canadian Solar HiKu 395W", 3, "CSH395-11223" },
                    { 4, 19.899999999999999, 4700.0, new DateOnly(2024, 8, 10), "Trina Solar Vertex 400W", 1, "TSV400-33445" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "Description", "EndDate", "MemberId", "ProjectId", "StartDate", "Status", "TaskName" },
                values: new object[,]
                {
                    { 1, "Conduct initial site assessment for solar panel installation.", new DateOnly(2024, 1, 15), 1, 1, new DateOnly(2024, 1, 10), "Completed", "Site Assessment" },
                    { 2, "Submit necessary permits for approval.", new DateOnly(2024, 2, 10), 2, 2, new DateOnly(2024, 2, 1), "In Progress", "Permit Submission" },
                    { 3, "Order and receive necessary materials for installation.", new DateOnly(2024, 3, 20), 3, 3, new DateOnly(2024, 3, 5), "Pending", "Material Procurement" },
                    { 4, "Complete solar panel installation on-site.", new DateOnly(2024, 4, 25), 2, 4, new DateOnly(2024, 4, 10), "Scheduled", "Installation" }
                });

            migrationBuilder.InsertData(
                table: "InstallationDetails",
                columns: new[] { "InstallationDetailId", "Description", "InstallCompleteDate", "InstallStartDate", "InstallationType", "LocationId", "NumberOfPanels", "ProjectId", "UserId" },
                values: new object[,]
                {
                    { 1, "Installation of 25 roof-mounted solar panels for residential project.", new DateOnly(2024, 3, 10), new DateOnly(2024, 3, 1), "Roof Mounted", 1, 25, 1, 1 },
                    { 2, "Ground-mounted solar panel system for commercial project.", new DateOnly(2024, 3, 20), new DateOnly(2024, 3, 15), "Ground Mounted", 2, 40, 2, 2 },
                    { 3, "Residential solar installation with 30 roof-mounted panels.", new DateOnly(2024, 4, 7), new DateOnly(2024, 4, 1), "Roof Mounted", 3, 30, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentation_ProjectId",
                table: "Documentation",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Financing_ProjectId",
                table: "Financing",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationDetails_LocationId",
                table: "InstallationDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationDetails_ProjectId",
                table: "InstallationDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationDetails_UserId",
                table: "InstallationDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationLocations_ProjectId",
                table: "InstallationLocations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_ProjectId",
                table: "Milestones",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ProjectId",
                table: "Notifications",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceHistory_PanelId",
                table: "PerformanceHistory",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_ProjectId",
                table: "Permits",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_ProjectId",
                table: "Predictions",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhases_ProjectId",
                table: "ProjectPhases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_HomeownerId",
                table: "Projects",
                column: "HomeownerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StatusId",
                table: "Projects",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TeamId",
                table: "Projects",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStages_ProjectId",
                table: "ProjectStages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "UQ__ProjectS__05E7698A0993BC6E",
                table: "ProjectStatus",
                column: "StatusName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolarPanels_ProjectId",
                table: "SolarPanels",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "UQ__SolarPan__048A0008DE638656",
                table: "SolarPanels",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_MemberId",
                table: "Tasks",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                table: "TeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "UQ__TeamMemb__96AB623464259E49",
                table: "TeamMembers",
                columns: new[] { "UserId", "TeamId" },
                unique: true,
                filter: "[UserId] IS NOT NULL AND [TeamId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UserId",
                table: "Teams",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__Teams__4E21CAACA71E3691",
                table: "Teams",
                column: "TeamName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E4E01254C0",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534859D3D96",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentation");

            migrationBuilder.DropTable(
                name: "Financing");

            migrationBuilder.DropTable(
                name: "InstallationDetails");

            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PerformanceHistory");

            migrationBuilder.DropTable(
                name: "Permits");

            migrationBuilder.DropTable(
                name: "Predictions");

            migrationBuilder.DropTable(
                name: "ProjectPhases");

            migrationBuilder.DropTable(
                name: "ProjectStages");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "InstallationLocations");

            migrationBuilder.DropTable(
                name: "SolarPanels");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Homeowners");

            migrationBuilder.DropTable(
                name: "ProjectStatus");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
