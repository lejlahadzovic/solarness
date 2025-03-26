using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Solarness.Services.Database;

public partial class SolarnessDbContext : DbContext
{
    public SolarnessDbContext()
    {
    }

    public SolarnessDbContext(DbContextOptions<SolarnessDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Documentation> Documentations { get; set; }

    public virtual DbSet<Financing> Financings { get; set; }

    public virtual DbSet<Homeowner> Homeowners { get; set; }

    public virtual DbSet<InstallationDetail> InstallationDetails { get; set; }

    public virtual DbSet<InstallationLocation> InstallationLocations { get; set; }

    public virtual DbSet<Milestone> Milestones { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PerformanceHistory> PerformanceHistories { get; set; }

    public virtual DbSet<Permit> Permits { get; set; }

    public virtual DbSet<Prediction> Predictions { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectPhase> ProjectPhases { get; set; }

    public virtual DbSet<ProjectStage> ProjectStages { get; set; }

    public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SolarPanel> SolarPanels { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=SolarnessDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Documentation>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF0FCDC0F7AC");

            entity.ToTable("Documentation");

            entity.Property(e => e.AdditionDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentLocation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DocumentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DocumentType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.Documentations)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Documenta__Proje__7F2BE32F");
        });

        modelBuilder.Entity<Financing>(entity =>
        {
            entity.HasKey(e => e.FinancingId).HasName("PK__Financin__706F686FB3BA6B0B");

            entity.ToTable("Financing");

            entity.Property(e => e.FinancingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FinancingName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.Financings)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Financing__Proje__44FF419A");
        });

        modelBuilder.Entity<Homeowner>(entity =>
        {
            entity.HasKey(e => e.HomeownerId).HasName("PK__Homeowne__8454E15114C8D373");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
        });

        modelBuilder.Entity<InstallationDetail>(entity =>
        {
            entity.HasKey(e => e.InstallationDetailId).HasName("PK__Installa__E00A47FA8D970B28");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.InstallationType)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Location).WithMany(p => p.InstallationDetails)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__Installat__Locat__797309D9");

            entity.HasOne(d => d.Project).WithMany(p => p.InstallationDetails)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Installat__Proje__7B5B524B");

            entity.HasOne(d => d.User).WithMany(p => p.InstallationDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Installat__UserI__7A672E12");
        });

        modelBuilder.Entity<InstallationLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Installa__E7FEA49765C8A2A2");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LocationName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.InstallationLocations)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Installat__Proje__628FA481");
        });

        modelBuilder.Entity<Milestone>(entity =>
        {
            entity.HasKey(e => e.MilestoneId).HasName("PK__Mileston__09C48078AD285774");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.MilestoneName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.Milestones)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Milestone__Proje__3C69FB99");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E1236416497");

            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.Title).HasColumnType("text");
            entity.Property(e => e.SendDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Notificat__Proje__5EBF139D");
        });

        modelBuilder.Entity<PerformanceHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Performa__4D7B4ABDE9D0F6E4");

            entity.ToTable("PerformanceHistory");

            entity.HasOne(d => d.Panel).WithMany(p => p.PerformanceHistories)
                .HasForeignKey(d => d.PanelId)
                .HasConstraintName("FK__Performan__Panel__5535A963");
        });

        modelBuilder.Entity<Permit>(entity =>
        {
            entity.HasKey(e => e.PermitId).HasName("PK__Permits__0B0E6DD0C6F2C103");

            entity.Property(e => e.PermitType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.Permits)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Permits__Project__3F466844");
        });

        modelBuilder.Entity<Prediction>(entity =>
        {
            entity.HasKey(e => e.PredictionId).HasName("PK__Predicti__BAE4C1A05C2174BA");

            entity.Property(e => e.PredictionStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.Predictions)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Predictio__Proje__59063A47");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABEF0EB7D0886");

            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContractAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Kw)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("KW");
            entity.Property(e => e.PriorityLevel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProjectDescription).HasColumnType("text");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Significance)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StreetAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Urgency)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Homeowner).WithMany(p => p.Projects)
                .HasForeignKey(d => d.HomeownerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Projects__Homeow__02FC7413");

            entity.HasOne(d => d.Status).WithMany(p => p.Projects)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Projects__Status__37A5467C");

            entity.HasOne(d => d.Team).WithMany(p => p.Projects)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Projects__TeamId__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.Projects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Projects__UserId__398D8EEE");
        });

        modelBuilder.Entity<ProjectPhase>(entity =>
        {
            entity.HasKey(e => e.PhaseId).HasName("PK__ProjectP__5BA26D62F4DD17A6");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.PhaseName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectPhases)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__ProjectPh__Proje__4CA06362");
        });

        modelBuilder.Entity<ProjectStage>(entity =>
        {
            entity.HasKey(e => e.StageId).HasName("PK__ProjectS__03EB7AD812943F9E");

            entity.Property(e => e.StageGroup)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StageName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectStages)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__ProjectSt__Proje__4222D4EF");
        });

        modelBuilder.Entity<ProjectStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__ProjectS__C8EE2063A9D2C0C0");

            entity.ToTable("ProjectStatus");

            entity.HasIndex(e => e.StatusName, "UQ__ProjectS__05E7698A0993BC6E").IsUnique();

            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AF69E740C");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<SolarPanel>(entity =>
        {
            entity.HasKey(e => e.PanelId).HasName("PK__SolarPan__49CA680639FACEF4");

            entity.HasIndex(e => e.SerialNumber, "UQ__SolarPan__048A0008DE638656").IsUnique();

            entity.Property(e => e.ModelName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.SolarPanels)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__SolarPane__Proje__5165187F");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B19478DBB1");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TaskName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Member).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Tasks__MemberId__49C3F6B7");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Tasks__ProjectId__48CFD27E");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__123AE7990851A463");

            entity.HasIndex(e => e.TeamName, "UQ__Teams__4E21CAACA71E3691").IsUnique();

            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.TeamName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Teams)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Teams__UserId__2D27B809");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__TeamMemb__0CF04B1815329411");

            entity.HasIndex(e => new { e.UserId, e.TeamId }, "UQ__TeamMemb__96AB623464259E49").IsUnique();

            entity.HasOne(d => d.Team).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__TeamMembe__TeamI__31EC6D26");

            entity.HasOne(d => d.User).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TeamMembe__UserI__30F848ED");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C477F1C10");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4E01254C0").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534859D3D96").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PasswordSalt).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Picture).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__286302EC");
        });

        // Seed Users
        modelBuilder.Entity<User>().HasData(
    new User
    {
        UserId = 1,
        FirstName = "John",
        LastName = "Doe",
        Username = "johny",
        Email = "johndoe@email.com",
        PhoneNumber = "123-456-7890",
        Picture = "path/to/picture.jpg",
        PasswordHash = "JfJzsL3ngGWki+Dn67C+8WLy73I=",  // Make sure to hash the password
        PasswordSalt = "7TUJfmgkkDvcY3PB/M4fhg==",   // Password salt
        RoleId = 1  // RoleId for Admin
    },
    new User
    {
        UserId = 2,
        FirstName = "Jane",
        LastName = "Smith",
        Username = "janes",
        Email = "janesmith@email.com",
        PhoneNumber = "098-765-4321",
        Picture = "path/to/picture2.jpg",
        PasswordHash = "ug0GgEnT5hKaHsfTn1l1kiGvZAg=",  // Make sure to hash the password
        PasswordSalt = "qh31pfpS2ox1h96QPhmR/Q==",   // Password salt
        RoleId = 3  // RoleId for User
    },
    new User
    {
        UserId = 3,
        FirstName = "Alice",
        LastName = "Snow",
        Username = "alice",
        Email = "alices@email.com",
        PhoneNumber = "123-456-7890",
        Picture = "path/to/picture.jpg",
        PasswordHash = "JfJzsL3ngGWki+Dn67C+8WLy73I=",  // Make sure to hash the password
        PasswordSalt = "7TUJfmgkkDvcY3PB/M4fhg==",   // Password salt
        RoleId = 2  // RoleId for Admin
    },
    
    new User
    {
        UserId = 4,
        FirstName = "Robert",
        LastName = "Johnson",
        Username = "robertj",
        Email = "robertj@email.com",
        PhoneNumber = "321-654-0987",
        Picture = "path/to/picture4.jpg",
        PasswordHash = "hJfJzsL3ngGWki+Dn67C+8WLy73I=",
        PasswordSalt = "9TUJfmgkkDvcY3PB/M4fhg==",
        RoleId = 2  // RoleId for Manager
    },
    new User
    {
        UserId = 5,
        FirstName = "Emily",
        LastName = "Clark",
        Username = "emilyc",
        Email = "emilyc@email.com",
        PhoneNumber = "456-123-7890",
        Picture = "path/to/picture5.jpg",
        PasswordHash = "oJfJzsL3ngGWki+Dn67C+8WLy73I=",
        PasswordSalt = "8TUJfmgkkDvcY3PB/M4fhg==",
        RoleId = 3  // RoleId for User
    },
    new User
    {
        UserId = 6,
        FirstName = "Daniel",
        LastName = "Brown",
        Username = "danbrown",
        Email = "danbrown@email.com",
        PhoneNumber = "987-654-3210",
        Picture = "path/to/picture6.jpg",
        PasswordHash = "pJfJzsL3ngGWki+Dn67C+8WLy73I=",
        PasswordSalt = "6TUJfmgkkDvcY3PB/M4fhg==",
        RoleId = 1  // RoleId for Admin
    },
    new User
    {
        UserId = 7,
        FirstName = "Sophia",
        LastName = "Miller",
        Username = "sophiam",
        Email = "sophiam@email.com",
        PhoneNumber = "789-012-3456",
        Picture = "path/to/picture7.jpg",
        PasswordHash = "qJfJzsL3ngGWki+Dn67C+8WLy73I=",
        PasswordSalt = "5TUJfmgkkDvcY3PB/M4fhg==",
        RoleId = 4  // RoleId for Manager
    },
    new User
    {
        UserId = 8,
        FirstName = "Michael",
        LastName = "Davis",
        Username = "michaeld",
        Email = "michaeld@email.com",
        PhoneNumber = "345-678-9012",
        Picture = "path/to/picture8.jpg",
        PasswordHash = "rJfJzsL3ngGWki+Dn67C+8WLy73I=",
        PasswordSalt = "4TUJfmgkkDvcY3PB/M4fhg==",
        RoleId = 3  // RoleId for User
    },
    new User
    {
        UserId = 9,
        FirstName = "Olivia",
        LastName = "Wilson",
        Username = "oliviaw",
        Email = "oliviaw@email.com",
        PhoneNumber = "654-321-0987",
        Picture = "path/to/picture9.jpg",
        PasswordHash = "sJfJzsL3ngGWki+Dn67C+8WLy73I=",
        PasswordSalt = "3TUJfmgkkDvcY3PB/M4fhg==",
        RoleId = 4  // RoleId for Admin
    },
    new User
    {
        UserId = 10,
        FirstName = "William",
        LastName = "Anderson",
        Username = "williamand",
        Email = "williamand@email.com",
        PhoneNumber = "231-546-7890",
        Picture = "path/to/picture10.jpg",
        PasswordHash = "tJfJzsL3ngGWki+Dn67C+8WLy73I=",
        PasswordSalt = "2TUJfmgkkDvcY3PB/M4fhg==",
        RoleId = 3  // RoleId for User
    },
    new User
    {
        UserId = 11,
        FirstName = "James",
        LastName = "Martinez",
        Username = "jamesm",
        Email = "jamesm@email.com",
        PhoneNumber = "123-789-4560",
        Picture = "path/to/picture11.jpg",
        PasswordHash = "uJfJzsL3ngGWki+Dn67C+8WLy73I=",
        PasswordSalt = "1TUJfmgkkDvcY3PB/M4fhg==",
        RoleId = 2  // RoleId for Manager
    },
    new User
    {
        UserId = 12,
        FirstName = "Isabella",
        LastName = "Garcia",
        Username = "isabellag",
        Email = "isabellag@email.com",
        PhoneNumber = "789-456-1230",
        Picture = "path/to/picture12.jpg",
        PasswordHash = "vJfJzsL3ngGWki+Dn67C+8WLy73I=",
        PasswordSalt = "0TUJfmgkkDvcY3PB/M4fhg==",
        RoleId = 3  // RoleId for User
    },
    new User
    {
        UserId = 13,
        FirstName = "David",
        LastName = "Taylor",
        Username = "davidt",
        Email = "davidt@email.com",
        PhoneNumber = "567-890-1234",
        Picture = "path/to/picture13.jpg",
        PasswordHash = "wJfJzsL3ngGWki+Dn67C+8WLy73I=",
        PasswordSalt = "zTUJfmgkkDvcY3PB/M4fhg==",
        RoleId = 4  
    });


        // Seed Roles
        modelBuilder.Entity<Role>().HasData(
     new Role
     {
         RoleId = 1,
         Name = "Admin",
         Description = "Administrator with full access to all system functionalities."
     },
     new Role
     {
         RoleId = 2,
         Name = "Manager",
         Description = "Responsible for managing projects and overseeing team members."
     },
     new Role
     {
         RoleId = 3,
         Name = "Panel installer",
         Description = "Regular user with access to view and interact with projects."
     },
     new Role
     {
         RoleId = 4,
         Name = "Technician",
         Description = "Responsible for the installation and maintenance of solar panels."
     }
 );


        // Seed Homeowners
        modelBuilder.Entity<Homeowner>().HasData(
    new Homeowner
    {
        HomeownerId = 1,
        FirstName = "John",
        LastName = "Doe",
        PhoneNumber = "123-456-7890",
        Email = "johndoe@email.com",
        Address = "123 Solar St.",
        City = "Solar City",
        PostalCode = "12345",
        Country = "SolarLand"
    },
    new Homeowner
    {
        HomeownerId = 2,
        FirstName = "Jane",
        LastName = "Smith",
        PhoneNumber = "987-654-3210",
        Email = "janesmith@email.com",
        Address = "456 Green Ave.",
        City = "Eco Town",
        PostalCode = "67890",
        Country = "EcoLand"
    },
    new Homeowner
    {
        HomeownerId = 3,
        FirstName = "Alice",
        LastName = "Johnson",
        PhoneNumber = "555-123-4567",
        Email = "alicejohnson@email.com",
        Address = "789 Windy Rd.",
        City = "Wind City",
        PostalCode = "11223",
        Country = "WindLand"
    },
    new Homeowner
    {
        HomeownerId = 4,
        FirstName = "Sam",
        LastName = "Sam",
        PhoneNumber = "555-123-4567",
        Email = "alicejohnson@email.com",
        Address = "789 Windy Rd.",
        City = "Wind City",
        PostalCode = "11223",
        Country = "WindLand"
    },
    new Homeowner
    {
        HomeownerId = 5,
        FirstName = "Michael",
        LastName = "Brown",
        PhoneNumber = "222-333-4444",
        Email = "michaelbrown@email.com",
        Address = "321 Sunbeam St.",
        City = "Solar City",
        PostalCode = "54321",
        Country = "SolarLand"
    },
    new Homeowner
    {
        HomeownerId = 6,
        FirstName = "Emily",
        LastName = "Davis",
        PhoneNumber = "111-222-3333",
        Email = "emilydavis@email.com",
        Address = "654 Eco Dr.",
        City = "Eco Town",
        PostalCode = "98765",
        Country = "EcoLand"
    },
    new Homeowner
    {
        HomeownerId = 7,
        FirstName = "Robert",
        LastName = "Wilson",
        PhoneNumber = "777-888-9999",
        Email = "robertwilson@email.com",
        Address = "147 Solar Panel Ln.",
        City = "Green Valley",
        PostalCode = "56789",
        Country = "SolarLand"
    },
    new Homeowner
    {
        HomeownerId = 8,
        FirstName = "Laura",
        LastName = "Martinez",
        PhoneNumber = "666-777-8888",
        Email = "lauramartinez@email.com",
        Address = "369 Renewable Ct.",
        City = "Eco Town",
        PostalCode = "34567",
        Country = "EcoLand"
    },
    new Homeowner
    {
        HomeownerId = 9,
        FirstName = "David",
        LastName = "Anderson",
        PhoneNumber = "444-555-6666",
        Email = "davidanderson@email.com",
        Address = "258 Sustainable St.",
        City = "Wind City",
        PostalCode = "23456",
        Country = "WindLand"
    },
    new Homeowner
    {
        HomeownerId = 10,
        FirstName = "Sophia",
        LastName = "Taylor",
        PhoneNumber = "888-999-0000",
        Email = "sophiataylor@email.com",
        Address = "159 Bright Future Blvd.",
        City = "Solar City",
        PostalCode = "67890",
        Country = "SolarLand"
    },
    new Homeowner
    {
        HomeownerId = 11,
        FirstName = "James",
        LastName = "Harris",
        PhoneNumber = "333-444-5555",
        Email = "jamesharris@email.com",
        Address = "753 Green Energy Ave.",
        City = "Eco Town",
        PostalCode = "13579",
        Country = "EcoLand"
    },
    new Homeowner
    {
        HomeownerId = 12,
        FirstName = "Isabella",
        LastName = "White",
        PhoneNumber = "222-111-0000",
        Email = "isabellawhite@email.com",
        Address = "951 Wind Power Rd.",
        City = "Wind City",
        PostalCode = "98712",
        Country = "WindLand"
    },
    new Homeowner
    {
        HomeownerId = 13,
        FirstName = "William",
        LastName = "Thompson",
        PhoneNumber = "777-666-5555",
        Email = "williamthompson@email.com",
        Address = "852 Solar Horizon St.",
        City = "Solar City",
        PostalCode = "36985",
        Country = "SolarLand"
    },
    new Homeowner
    {
        HomeownerId = 14,
        FirstName = "Olivia",
        LastName = "Garcia",
        PhoneNumber = "999-888-7777",
        Email = "oliviagarcia@email.com",
        Address = "456 Renewable Future Blvd.",
        City = "Green Valley",
        PostalCode = "21478",
        Country = "EcoLand"
    }
);


        // Seed Projects
        modelBuilder.Entity<Project>().HasData(
    new Project
    {
        ProjectId = 1,
        ProjectName = "Solar Panel Installation for Residential Home",
        ProjectDescription = "This project involves the installation of a solar panel system for a residential home located in the city center.",
        StreetAddress = "123 Solar St.",
        City = "Solar City",
        Kw = 15.5m, // 15.5 kW
        ContractAmount = 25000.00m,
        SiteInspectionDate = new DateOnly(2025, 3, 10),
        EngineeringSubmitDate = new DateOnly(2025, 3, 15),
        EngineeringReceivedDate = new DateOnly(2025, 3, 20),
        SaleDate = new DateOnly(2025, 2, 5),
        Significance = "High",
        Urgency = "Medium",
        PriorityLevel = "High",
        StatusId = 1, // "Planning"
        TeamId = 1, // "Solar Installers" team
        UserId = 1, // Admin user
        HomeownerId = 1, // Homeowner with ID 1
    },
    new Project
    {
        ProjectId = 2,
        ProjectName = "Commercial Solar Panel Project",
        ProjectDescription = "A commercial solar panel installation for a local office building.",
        StreetAddress = "456 Green Ave.",
        City = "Solar City",
        Kw = 100.0m, // 100 kW
        ContractAmount = 500000.00m,
        SiteInspectionDate = new DateOnly(2025, 4, 5),
        EngineeringSubmitDate = new DateOnly(2025, 4, 10),
        EngineeringReceivedDate = new DateOnly(2025, 4, 12),
        SaleDate = new DateOnly(2025, 3, 1),
        Significance = "Medium",
        Urgency = "Low",
        PriorityLevel = "Medium",
        StatusId = 2, // "In Progress"
        TeamId = 2, // "Commercial Solar Team"
        UserId = 2, // Another admin user
        HomeownerId = 2, // Commercial property owner
    },
     new Project
     {
         ProjectId = 3,
         ProjectName = "Solar Panel Installation for Company",
         ProjectDescription = "This project involves the installation of a solar panel system for a residential home located in the city center.",
         StreetAddress = "123 Solar St.",
         City = "Solar City",
         Kw = 15.5m, // 15.5 kW
         ContractAmount = 25000.00m,
         SiteInspectionDate = new DateOnly(2025, 3, 10),
         EngineeringSubmitDate = new DateOnly(2025, 3, 15),
         EngineeringReceivedDate = new DateOnly(2025, 3, 20),
         SaleDate = new DateOnly(2025, 2, 5),
         Significance = "High",
         Urgency = "Medium",
         PriorityLevel = "High",
         StatusId = 1, // "Planning"
         TeamId = 1, // "Solar Installers" team
         UserId = 1, // Admin user
         HomeownerId = 3, // Homeowner with ID 1
     },
      new Project
      {
          ProjectId = 4,
          ProjectName = "Solar Panel and system Installation ",
          ProjectDescription = "This project involves the installation of a solar panel system for a residential home located in the city center.",
          StreetAddress = "123 Solar St.",
          City = "Solar City",
          Kw = 15.5m, // 15.5 kW
          ContractAmount = 25000.00m,
          SiteInspectionDate = new DateOnly(2025, 3, 10),
          EngineeringSubmitDate = new DateOnly(2025, 3, 15),
          EngineeringReceivedDate = new DateOnly(2025, 3, 20),
          SaleDate = new DateOnly(2025, 2, 5),
          Significance = "High",
          Urgency = "Medium",
          PriorityLevel = "High",
          StatusId = 1, // "Planning"
          TeamId = 1, // "Solar Installers" team
          UserId = 1, // Admin user
          HomeownerId = 4, // Homeowner with ID 1
      },
       new Project
       {
           ProjectId = 5,
           ProjectName = "Rooftop Solar for Shopping Mall",
           ProjectDescription = "Installation of a large-scale rooftop solar system for a shopping mall.",
           StreetAddress = "789 Mall Rd.",
           City = "Solar City",
           Kw = 200.0m, // 200 kW
           ContractAmount = 800000.00m,
           SiteInspectionDate = new DateOnly(2025, 5, 10),
           EngineeringSubmitDate = new DateOnly(2025, 5, 15),
           EngineeringReceivedDate = new DateOnly(2025, 5, 18),
           SaleDate = new DateOnly(2025, 4, 1),
           Significance = "Very High",
           Urgency = "High",
           PriorityLevel = "Very High",
           StatusId = 3, // "Approved"
           TeamId = 3, // "Mall Solar Team"
           UserId = 3, // Project Manager
           HomeownerId = 5, // Mall Owner
       },
    new Project
    {
        ProjectId = 6,
        ProjectName = "Solar Farm for Agricultural Use",
        ProjectDescription = "A solar farm project to provide renewable energy for a large agricultural facility.",
        StreetAddress = "567 Farm Ln.",
        City = "Green Valley",
        Kw = 500.0m, // 500 kW
        ContractAmount = 1500000.00m,
        SiteInspectionDate = new DateOnly(2025, 6, 1),
        EngineeringSubmitDate = new DateOnly(2025, 6, 5),
        EngineeringReceivedDate = new DateOnly(2025, 6, 10),
        SaleDate = new DateOnly(2025, 5, 1),
        Significance = "High",
        Urgency = "Medium",
        PriorityLevel = "High",
        StatusId = 4, // "Under Construction"
        TeamId = 4, // "Solar Farms Team"
        UserId = 4, // Lead Engineer
        HomeownerId = 6, // Farm Owner
    },
    new Project
    {
        ProjectId = 7,
        ProjectName = "Off-Grid Solar Installation for Cabin",
        ProjectDescription = "Installation of a completely off-grid solar system for a remote cabin.",
        StreetAddress = "910 Mountain Pass",
        City = "Lakewood",
        Kw = 10.0m, // 10 kW
        ContractAmount = 20000.00m,
        SiteInspectionDate = new DateOnly(2025, 7, 1),
        EngineeringSubmitDate = new DateOnly(2025, 7, 5),
        EngineeringReceivedDate = new DateOnly(2025, 7, 8),
        SaleDate = new DateOnly(2025, 6, 10),
        Significance = "Medium",
        Urgency = "High",
        PriorityLevel = "Medium",
        StatusId = 2, // "In Progress"
        TeamId = 5, // "Off-Grid Solar Team"
        UserId = 5, // Sales Manager
        HomeownerId = 7, // Cabin Owner
    },
    new Project
    {
        ProjectId = 8,
        ProjectName = "Solar Backup System for Hospital",
        ProjectDescription = "Installation of a solar-powered backup system for a major city hospital.",
        StreetAddress = "300 Health St.",
        City = "Solar City",
        Kw = 300.0m, // 300 kW
        ContractAmount = 1200000.00m,
        SiteInspectionDate = new DateOnly(2025, 8, 15),
        EngineeringSubmitDate = new DateOnly(2025, 8, 20),
        EngineeringReceivedDate = new DateOnly(2025, 8, 25),
        SaleDate = new DateOnly(2025, 7, 10),
        Significance = "Very High",
        Urgency = "Very High",
        PriorityLevel = "Very High",
        StatusId = 5, // "Completed"
        TeamId = 6, // "Hospital Solar Team"
        UserId = 6, // Admin
        HomeownerId = 8, // Hospital Owner
    },
    new Project
    {
        ProjectId = 9,
        ProjectName = "Residential Solar with Battery Storage",
        ProjectDescription = "Installation of a solar panel system with battery storage for a suburban home.",
        StreetAddress = "222 Suburb Ln.",
        City = "Solar City",
        Kw = 20.0m, // 20 kW
        ContractAmount = 30000.00m,
        SiteInspectionDate = new DateOnly(2025, 9, 5),
        EngineeringSubmitDate = new DateOnly(2025, 9, 10),
        EngineeringReceivedDate = new DateOnly(2025, 9, 12),
        SaleDate = new DateOnly(2025, 8, 1),
        Significance = "Medium",
        Urgency = "Medium",
        PriorityLevel = "Medium",
        StatusId = 1, // "Planning"
        TeamId = 1, // "Solar Installers"
        UserId = 7, // Technician
        HomeownerId = 9, // Homeowner
    },
    new Project
    {
        ProjectId = 10,
        ProjectName = "Solar Canopy for Public Park",
        ProjectDescription = "A solar canopy providing renewable energy for a local public park.",
        StreetAddress = "100 Park Blvd.",
        City = "Green Town",
        Kw = 50.0m, // 50 kW
        ContractAmount = 200000.00m,
        SiteInspectionDate = new DateOnly(2025, 10, 10),
        EngineeringSubmitDate = new DateOnly(2025, 10, 15),
        EngineeringReceivedDate = new DateOnly(2025, 10, 18),
        SaleDate = new DateOnly(2025, 9, 1),
        Significance = "High",
        Urgency = "Low",
        PriorityLevel = "Medium",
        StatusId = 3, // "Approved"
        TeamId = 2, // "Commercial Solar Team"
        UserId = 8, // Project Manager
        HomeownerId = 10, // City Government
    },
    new Project
    {
        ProjectId = 11,
        ProjectName = "Solar Retrofit for Factory",
        ProjectDescription = "Adding solar panels to an existing factory to reduce energy costs.",
        StreetAddress = "888 Industrial Ave.",
        City = "Industrial City",
        Kw = 400.0m, // 400 kW
        ContractAmount = 1600000.00m,
        SiteInspectionDate = new DateOnly(2025, 11, 5),
        EngineeringSubmitDate = new DateOnly(2025, 11, 10),
        EngineeringReceivedDate = new DateOnly(2025, 11, 12),
        SaleDate = new DateOnly(2025, 10, 1),
        Significance = "Very High",
        Urgency = "Medium",
        PriorityLevel = "High",
        StatusId = 4, // "Under Construction"
        TeamId = 4, // "Industrial Solar Team"
        UserId = 9, // Senior Engineer
        HomeownerId = 11, // Factory Owner
    },
    new Project
    {
        ProjectId = 12,
        ProjectName = "Solar Streetlight Installation",
        ProjectDescription = "Installation of solar-powered streetlights across the city.",
        StreetAddress = "Various Locations",
        City = "Solar City",
        Kw = 80.0m, // 80 kW
        ContractAmount = 300000.00m,
        SiteInspectionDate = new DateOnly(2025, 12, 1),
        EngineeringSubmitDate = new DateOnly(2025, 12, 5),
        EngineeringReceivedDate = new DateOnly(2025, 12, 10),
        SaleDate = new DateOnly(2025, 11, 1),
        Significance = "Medium",
        Urgency = "High",
        PriorityLevel = "High",
        StatusId = 2, // "In Progress"
        TeamId = 3, // "Public Infrastructure Team"
        UserId = 10, // Project Manager
        HomeownerId = 12, // City Council
    }
);


        // Seed Project Statuses
        modelBuilder.Entity<ProjectStatus>().HasData(
    new ProjectStatus
    {
        StatusId = 1,
        StatusName = "Planning"
    },
    new ProjectStatus
    {
        StatusId = 2,
        StatusName = "In Progress"
    },
    new ProjectStatus
    {
        StatusId = 3,
        StatusName = "Completed"
    },
    new ProjectStatus
    {
        StatusId = 4,
        StatusName = "On Hold"
    },
    new ProjectStatus
    {
        StatusId = 5,
        StatusName = "Cancelled"
    }
);


        // Seed Solar Panels
        modelBuilder.Entity<SolarPanel>().HasData(
     new SolarPanel
     {
         PanelId = 1,
         ModelName = "SunPower X22-370",
         SerialNumber = "SPX22-370-12345",
         ProjectId = 1,
         InstallationDate = new DateOnly(2024, 5, 15),
         Efficiency = 22.7,
         EnergyProduced = 5000
     },
     new SolarPanel
     {
         PanelId = 2,
         ModelName = "LG NeON R 370W",
         SerialNumber = "LGNR-370-67890",
         ProjectId = 4,
         InstallationDate = new DateOnly(2024, 6, 5),
         Efficiency = 21.4,
         EnergyProduced = 4000
     },
     new SolarPanel
     {
         PanelId = 3,
         ModelName = "Canadian Solar HiKu 395W",
         SerialNumber = "CSH395-11223",
         ProjectId = 3,
         InstallationDate = new DateOnly(2024, 7, 8),
         Efficiency = 20.5,
         EnergyProduced = 4500
     },
     new SolarPanel
     {
         PanelId = 4,
         ModelName = "Trina Solar Vertex 400W",
         SerialNumber = "TSV400-33445",
         ProjectId = 1,
         InstallationDate = new DateOnly(2024, 8, 10),
         Efficiency = 19.9,
         EnergyProduced = 4700
     }
 );


        // Seed Installation Locations
        modelBuilder.Entity<InstallationLocation>().HasData(
    new InstallationLocation
    {
        LocationId = 1,
        LocationName = "Solar Installation Site 1",
        Address = "123 Solar St, Green Valley",
        City = "Green Valley",
        Country = "USA",
        Latitude = 34.0522,
        Longitude = -118.2437,
        ProjectId = 1
    },
    new InstallationLocation
    {
        LocationId = 2,
        LocationName = "Solar Installation Site 2",
        Address = "456 Sunshine Rd, Tech Park",
        City = "Tech City",
        Country = "USA",
        Latitude = 40.7128,
        Longitude = -74.0060,
        ProjectId = 2
    },
    new InstallationLocation
    {
        LocationId = 3,
        LocationName = "Solar Installation Site 3",
        Address = "789 Bright Ave, Oak Town",
        City = "Oak Town",
        Country = "Canada",
        Latitude = 43.6517,
        Longitude = -79.3832,
        ProjectId = 3
    },
    new InstallationLocation
    {
        LocationId = 4,
        LocationName = "Solar Installation Site 4",
        Address = "101 Sunbeam Blvd, River City",
        City = "River City",
        Country = "Canada",
        Latitude = 45.4215,
        Longitude = -75.6992,
        ProjectId = 4
    }
);


        // Seed Installation Details
        modelBuilder.Entity<InstallationDetail>().HasData(
     new InstallationDetail
     {
         InstallationDetailId = 1,
         LocationId = 1,
         UserId = 1,
         InstallStartDate = new DateOnly(2024, 3, 1),
         InstallCompleteDate = new DateOnly(2024, 3, 10),
         InstallationType = "Roof Mounted",
         NumberOfPanels = 25,
         Description = "Installation of 25 roof-mounted solar panels for residential project.",
         ProjectId = 1
     },
     new InstallationDetail
     {
         InstallationDetailId = 2,
         LocationId = 2,
         UserId = 2,
         InstallStartDate = new DateOnly(2024, 3, 15),
         InstallCompleteDate = new DateOnly(2024, 3, 20),
         InstallationType = "Ground Mounted",
         NumberOfPanels = 40,
         Description = "Ground-mounted solar panel system for commercial project.",
         ProjectId = 2
     },
     new InstallationDetail
     {
         InstallationDetailId = 3,
         LocationId = 3,
         UserId = 3,
         InstallStartDate = new DateOnly(2024, 4, 1),
         InstallCompleteDate = new DateOnly(2024, 4, 7),
         InstallationType = "Roof Mounted",
         NumberOfPanels = 30,
         Description = "Residential solar installation with 30 roof-mounted panels.",
         ProjectId = 3
     },
     new InstallationDetail
     {
         InstallationDetailId = 4,
         LocationId = 4,
         UserId = 4,
         InstallStartDate = new DateOnly(2024, 4, 5),
         InstallCompleteDate = new DateOnly(2024, 4, 12),
         InstallationType = "Ground Mounted",
         NumberOfPanels = 50,
         Description = "Installation of 50 ground-mounted solar panels at a commercial site.",
         ProjectId = 4
     }
 );


        // Seed Milestones
        modelBuilder.Entity<Milestone>().HasData(
    new Milestone
    {
        MilestoneId = 1,
        ProjectId = 1,
        MilestoneName = "Project Kickoff",
        MilestoneDate = new DateOnly(2024, 1, 1),
        Description = "The project officially kicks off with team and stakeholder introductions."
    },
    new Milestone
    {
        MilestoneId = 2,
        ProjectId = 1,
        MilestoneName = "Design Approval",
        MilestoneDate = new DateOnly(2024, 2, 15),
        Description = "Final designs for the solar panel layout are approved."
    },
    new Milestone
    {
        MilestoneId = 3,
        ProjectId = 2,
        MilestoneName = "Permit Approval",
        MilestoneDate = new DateOnly(2024, 3, 5),
        Description = "The necessary permits are approved for installation."
    },
    new Milestone
    {
        MilestoneId = 4,
        ProjectId = 2,
        MilestoneName = "Material Procurement",
        MilestoneDate = new DateOnly(2024, 3, 25),
        Description = "All required materials for installation are procured."
    },
    new Milestone
    {
        MilestoneId = 5,
        ProjectId = 3,
        MilestoneName = "Site Preparation",
        MilestoneDate = new DateOnly(2024, 4, 10),
        Description = "The site is prepared for solar panel installation."
    }
);


        // Seed Tasks
        modelBuilder.Entity<Task>().HasData(
    new Task
    {
        TaskId = 1,
        TaskName = "Site Assessment",
        Description = "Conduct initial site assessment for solar panel installation.",
        StartDate = new DateOnly(2024, 1, 10),
        EndDate = new DateOnly(2024, 1, 15),
        Status = "Completed",
        ProjectId = 1,
        MemberId = 1
    },
    new Task
    {
        TaskId = 2,
        TaskName = "Permit Submission",
        Description = "Submit necessary permits for approval.",
        StartDate = new DateOnly(2024, 2, 1),
        EndDate = new DateOnly(2024, 2, 10),
        Status = "In Progress",
        ProjectId = 2,
        MemberId = 2
    },
    new Task
    {
        TaskId = 3,
        TaskName = "Material Procurement",
        Description = "Order and receive necessary materials for installation.",
        StartDate = new DateOnly(2024, 3, 5),
        EndDate = new DateOnly(2024, 3, 20),
        Status = "Pending",
        ProjectId = 3,
        MemberId = 3
    },
    new Task
    {
        TaskId = 4,
        TaskName = "Installation",
        Description = "Complete solar panel installation on-site.",
        StartDate = new DateOnly(2024, 4, 10),
        EndDate = new DateOnly(2024, 4, 25),
        Status = "Scheduled",
        ProjectId = 4,
        MemberId = 2
    },
    new Task
    {
        TaskId = 5,
        TaskName = "System Testing",
        Description = "Conduct system tests to ensure proper functionality.",
        StartDate = new DateOnly(2024, 5, 1),
        EndDate = new DateOnly(2024, 5, 5),
        Status = "Scheduled",
        ProjectId = 1,
        MemberId = 4
    },
    new Task
    {
        TaskId = 6,
        TaskName = "Final Inspection",
        Description = "Perform final inspection before project completion.",
        StartDate = new DateOnly(2024, 5, 10),
        EndDate = new DateOnly(2024, 5, 15),
        Status = "Pending",
        ProjectId = 2,
        MemberId = 5
    },
    new Task
    {
        TaskId = 7,
        TaskName = "Client Training",
        Description = "Train the client on solar panel maintenance and usage.",
        StartDate = new DateOnly(2024, 6, 1),
        EndDate = new DateOnly(2024, 6, 2),
        Status = "Pending",
        ProjectId = 3,
        MemberId = 6
    },
    new Task
    {
        TaskId = 8,
        TaskName = "Electrical Work",
        Description = "Complete necessary electrical work for installation.",
        StartDate = new DateOnly(2024, 6, 5),
        EndDate = new DateOnly(2024, 6, 10),
        Status = "Scheduled",
        ProjectId = 4,
        MemberId = 7
    },
    new Task
    {
        TaskId = 9,
        TaskName = "Roof Preparation",
        Description = "Prepare the roof for solar panel installation.",
        StartDate = new DateOnly(2024, 7, 1),
        EndDate = new DateOnly(2024, 7, 3),
        Status = "Completed",
        ProjectId = 5,
        MemberId = 8
    },
    new Task
    {
        TaskId = 10,
        TaskName = "Structural Reinforcement",
        Description = "Ensure the structure can support solar panels.",
        StartDate = new DateOnly(2024, 7, 5),
        EndDate = new DateOnly(2024, 7, 8),
        Status = "In Progress",
        ProjectId = 6,
        MemberId = 9
    },
    new Task
    {
        TaskId = 11,
        TaskName = "Cable Management",
        Description = "Organize and secure all necessary cabling.",
        StartDate = new DateOnly(2024, 8, 1),
        EndDate = new DateOnly(2024, 8, 3),
        Status = "Pending",
        ProjectId = 7,
        MemberId = 10
    },
    new Task
    {
        TaskId = 12,
        TaskName = "Solar Panel Mounting",
        Description = "Secure solar panels onto the mounting structure.",
        StartDate = new DateOnly(2024, 8, 5),
        EndDate = new DateOnly(2024, 8, 10),
        Status = "Scheduled",
        ProjectId = 8,
        MemberId = 1
    },
    new Task
    {
        TaskId = 13,
        TaskName = "System Integration",
        Description = "Integrate solar system with the existing power grid.",
        StartDate = new DateOnly(2024, 9, 1),
        EndDate = new DateOnly(2024, 9, 5),
        Status = "Pending",
        ProjectId = 9,
        MemberId = 2
    },
    new Task
    {
        TaskId = 14,
        TaskName = "Warranty and Documentation",
        Description = "Provide warranty details and project documentation to the client.",
        StartDate = new DateOnly(2024, 9, 10),
        EndDate = new DateOnly(2024, 9, 15),
        Status = "Scheduled",
        ProjectId = 10,
        MemberId = 3
    }
);


        // Seed Financing
        modelBuilder.Entity<Financing>().HasData(
     new Financing
     {
         FinancingId = 1,
         ProjectId = 1,
         FinancingName = "Government Subsidy",
         FinancingAmount = 50000.00m,
         FinancingDate = new DateOnly(2024, 1, 15)
     },
     new Financing
     {
         FinancingId = 2,
         ProjectId = 2,
         FinancingName = "Bank Loan",
         FinancingAmount = 75000.00m,
         FinancingDate = new DateOnly(2024, 2, 20)
     },
     new Financing
     {
         FinancingId = 3,
         ProjectId = 3,
         FinancingName = "Private Investment",
         FinancingAmount = 100000.00m,
         FinancingDate = new DateOnly(2024, 3, 5)
     },
     new Financing
     {
         FinancingId = 4,
         ProjectId = 4,
         FinancingName = "Crowdfunding",
         FinancingAmount = 25000.00m,
         FinancingDate = new DateOnly(2024, 4, 10)
     }
 );


        // Seed Documentation
        modelBuilder.Entity<Documentation>().HasData(
    new Documentation
    {
        DocumentId = 1,
        DocumentName = "Site Plan",
        DocumentType = "PDF",
        DocumentLocation = "/documents/site_plan_1.pdf",
        ProjectId = 1,
        AdditionDate = new DateOnly(2024, 1, 10)
    },
    new Documentation
    {
        DocumentId = 2,
        DocumentName = "Permit Approval",
        DocumentType = "PDF",
        DocumentLocation = "/documents/permit_approval_1.pdf",
        ProjectId = 1,
        AdditionDate = new DateOnly(2024, 2, 5)
    },
    new Documentation
    {
        DocumentId = 3,
        DocumentName = "Electrical Layout",
        DocumentType = "DWG",
        DocumentLocation = "/documents/electrical_layout_2.dwg",
        ProjectId = 2,
        AdditionDate = new DateOnly(2024, 3, 15)
    },
    new Documentation
    {
        DocumentId = 4,
        DocumentName = "Inspection Report",
        DocumentType = "DOCX",
        DocumentLocation = "/documents/inspection_report_3.docx",
        ProjectId = 3,
        AdditionDate = new DateOnly(2024, 4, 2)
    }
);


        // Seed Permits
        modelBuilder.Entity<Permit>().HasData(
     new Permit
     {
         PermitId = 1,
         ProjectId = 1,
         PermitSubmitDate = new DateOnly(2024, 2, 10),
         PermitReceivedDate = new DateOnly(2024, 2, 20),
         PermitType = "Construction",
         Status = "Approved",
         LastUpdatedDate = new DateOnly(2024, 2, 21)
     },
     new Permit
     {
         PermitId = 2,
         ProjectId = 2,
         PermitSubmitDate = new DateOnly(2024, 3, 1),
         PermitReceivedDate = null,  // Still in process
         PermitType = "Environmental",
         Status = "Pending",
         LastUpdatedDate = new DateOnly(2024, 3, 5)
     },
     new Permit
     {
         PermitId = 3,
         ProjectId = 3,
         PermitSubmitDate = new DateOnly(2024, 1, 15),
         PermitReceivedDate = new DateOnly(2024, 1, 30),
         PermitType = "Electrical",
         Status = "Approved",
         LastUpdatedDate = new DateOnly(2024, 2, 1)
     }
 );


        // Seed Notifications
        modelBuilder.Entity<Notification>().HasData(
    new Notification
    {
        NotificationId = 1,
        ProjectId = 1,
        Title = "Project Approved",
        Content = "Your solar project has been approved and is ready for the next phase.",
        SendDate = new DateTime(2024, 3, 1, 10, 0, 0)
    },
    new Notification
    {
        NotificationId = 2,
        ProjectId = 2,
        Title = "Installation Scheduled",
        Content = "The installation for your project is scheduled for next week.",
        SendDate = new DateTime(2024, 3, 5, 14, 30, 0)
    },
    new Notification
    {
        NotificationId = 3,
        ProjectId = 1,
        Title = "Final Inspection",
        Content = "Your solar panel installation has been completed. A final inspection is scheduled.",
        SendDate = new DateTime(2024, 3, 10, 9, 15, 0)
    }
);

        // Seed Performance History
        //modelBuilder.Entity<PerformanceHistory>().HasData(
        //    new PerformanceHistory { Id = 1, ProjectId = 1, EnergyGenerated = 500, DateRecorded = DateTime.UtcNow }
        //);

        // Seed Predictions
        //modelBuilder.Entity<Prediction>().HasData(
        //    new Prediction { Id = 1, ProjectId = 1, PredictedEnergyOutput = 5200, PredictionDate = DateTime.UtcNow }
        //);

        // Seed Teams
        modelBuilder.Entity<Team>().HasData(
    new Team
    {
        TeamId = 1,
        TeamName = "Solar Installers",
        Description = "Team responsible for installing solar panels.",
        CreationDate = new DateTime(2024, 1, 15),
        UserId = 1
    },
    new Team
    {
        TeamId = 2,
        TeamName = "Project Engineers",
        Description = "Engineers overseeing solar panel projects.",
        CreationDate = new DateTime(2024, 2, 10),
        UserId = 2
    },
    new Team
    {
        TeamId = 3,
        TeamName = "Electrical Technicians",
        Description = "Team responsible for handling electrical components of solar projects.",
        CreationDate = new DateTime(2024, 3, 5),
        UserId = 3
    },
    new Team
    {
        TeamId = 4,
        TeamName = "Site Inspectors",
        Description = "Team ensuring project sites meet installation requirements.",
        CreationDate = new DateTime(2024, 3, 20),
        UserId = 4
    },
    new Team
    {
        TeamId = 5,
        TeamName = "Sales & Client Relations",
        Description = "Team handling sales and customer relationships.",
        CreationDate = new DateTime(2024, 4, 15),
        UserId = 5
    },
    new Team
    {
        TeamId = 6,
        TeamName = "Permitting Specialists",
        Description = "Team responsible for securing necessary permits.",
        CreationDate = new DateTime(2024, 5, 2),
        UserId = 6
    },
    new Team
    {
        TeamId = 7,
        TeamName = "Material Procurement",
        Description = "Team handling orders and materials for projects.",
        CreationDate = new DateTime(2024, 5, 18),
        UserId = 7
    },
    new Team
    {
        TeamId = 8,
        TeamName = "Quality Assurance",
        Description = "Team responsible for checking installation quality.",
        CreationDate = new DateTime(2024, 6, 10),
        UserId = 8
    },
    new Team
    {
        TeamId = 9,
        TeamName = "Maintenance & Support",
        Description = "Team providing maintenance and troubleshooting support.",
        CreationDate = new DateTime(2024, 6, 25),
        UserId = 9
    },
    new Team
    {
        TeamId = 10,
        TeamName = "Renewable Energy Consultants",
        Description = "Team advising on energy efficiency and renewable solutions.",
        CreationDate = new DateTime(2024, 7, 5),
        UserId = 10
    },
    new Team
    {
        TeamId = 11,
        TeamName = "Battery Storage Experts",
        Description = "Team specializing in battery storage solutions for solar projects.",
        CreationDate = new DateTime(2024, 7, 20),
        UserId = 11
    },
    new Team
    {
        TeamId = 12,
        TeamName = "Grid Integration Specialists",
        Description = "Team handling integration of solar systems with the power grid.",
        CreationDate = new DateTime(2024, 8, 10),
        UserId = 12
    }
);


        // Seed Team Members
        modelBuilder.Entity<TeamMember>().HasData(
    new TeamMember
    {
        MemberId = 1,
        TeamId = 1,
        UserId = 1
    },
    new TeamMember
    {
        MemberId = 2,
        TeamId = 1,
        UserId = 2
    },
    new TeamMember
    {
        MemberId = 3,
        TeamId = 2,
        UserId = 3
    },
     new TeamMember
     {
         MemberId = 4,
         TeamId = 1,
         UserId = 12
     },
    new TeamMember
    {
        MemberId = 5,
        TeamId = 3,
        UserId = 11
    },
    new TeamMember
    {
        MemberId = 6,
        TeamId = 2,
        UserId = 10
    },
     new TeamMember
     {
         MemberId = 7,
         TeamId = 1,
         UserId = 9
     },
    new TeamMember
    {
        MemberId = 8,
        TeamId = 5,
        UserId = 8
    },
    new TeamMember
    {
        MemberId = 9,
        TeamId = 5,
        UserId = 7
    },
     new TeamMember
     {
         MemberId = 10,
         TeamId = 5,
         UserId = 6
     },
    new TeamMember
    {
        MemberId = 11,
        TeamId = 4,
        UserId = 5
    },
    new TeamMember
    {
        MemberId = 12,
        TeamId = 1,
        UserId = 4
    }
);

        // Seed Project Phases
        modelBuilder.Entity<ProjectPhase>().HasData(
    new ProjectPhase
    {
        PhaseId = 1,
        ProjectId = 1,
        PhaseName = "Planning",
        Description = "Initial phase for project feasibility and design",
        StartDate = new DateOnly(2024, 2, 1),
        EndDate = new DateOnly(2024, 2, 28),
        Status = "Completed"
    },
    new ProjectPhase
    {
        PhaseId = 2,
        ProjectId = 1,
        PhaseName = "Permitting",
        Description = "Regulatory approvals and paperwork processing",
        StartDate = new DateOnly(2024, 3, 1),
        EndDate = new DateOnly(2024, 3, 20),
        Status = "In Progress"
    },
    new ProjectPhase
    {
        PhaseId = 3,
        ProjectId = 1,
        PhaseName = "Installation",
        Description = "Solar panels and system installation on-site",
        StartDate = new DateOnly(2024, 3, 22),
        EndDate = new DateOnly(2024, 4, 10),
        Status = "Not Started"
    }
);


        // Seed Project Stages
        modelBuilder.Entity<ProjectStage>().HasData(
            new ProjectStage
            {
                StageId = 1,
                ProjectId = 1,
                StageName = "Initial Site Survey",
                StageGroup = "Planning",
                StageStartDate = new DateOnly(2024, 3, 1),
                StageEndDate = new DateOnly(2024, 3, 5),
                DaysInStage = 5,
                StageUpdatedDate = new DateOnly(2024, 3, 6)
            },
    new ProjectStage
    {
        StageId = 2,
        ProjectId = 1,
        StageName = "Permit Approval",
        StageGroup = "Regulatory",
        StageStartDate = new DateOnly(2024, 3, 6),
        StageEndDate = new DateOnly(2024, 3, 20),
        DaysInStage = 15,
        StageUpdatedDate = new DateOnly(2024, 3, 21)
    },
    new ProjectStage
    {
        StageId = 3,
        ProjectId = 1,
        StageName = "Panel Installation",
        StageGroup = "Construction",
        StageStartDate = new DateOnly(2024, 3, 22),
        StageEndDate = new DateOnly(2024, 4, 1),
        DaysInStage = 10,
        StageUpdatedDate = new DateOnly(2024, 4, 2)
    }
        );

        OnModelCreatingPartial(modelBuilder);


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
