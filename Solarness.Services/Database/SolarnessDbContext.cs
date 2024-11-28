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
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Milestone__Proje__3C69FB99");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E1236416497");

            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.SendDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Notificat__Proje__5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Notificat__UserI__5DCAEF64");
        });

        modelBuilder.Entity<PerformanceHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Performa__4D7B4ABDE9D0F6E4");

            entity.ToTable("PerformanceHistory");

            entity.HasOne(d => d.Panel).WithMany(p => p.PerformanceHistories)
                .HasForeignKey(d => d.PanelId)
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tasks__MemberId__49C3F6B7");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TeamMembe__TeamI__31EC6D26");

            entity.HasOne(d => d.User).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
