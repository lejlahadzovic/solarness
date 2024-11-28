using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class Project
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string? ProjectDescription { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public decimal? Kw { get; set; }

    public decimal? ContractAmount { get; set; }

    public DateOnly? SiteInspectionDate { get; set; }

    public DateOnly? EngineeringSubmitDate { get; set; }

    public DateOnly? EngineeringReceivedDate { get; set; }

    public DateOnly? SaleDate { get; set; }

    public string? Significance { get; set; }

    public string? Urgency { get; set; }

    public string? PriorityLevel { get; set; }

    public int? StatusId { get; set; }

    public int? TeamId { get; set; }

    public int? UserId { get; set; }

    public int? HomeownerId { get; set; }

    public virtual ICollection<Documentation> Documentations { get; set; } = new List<Documentation>();

    public virtual ICollection<Financing> Financings { get; set; } = new List<Financing>();

    public virtual Homeowner? Homeowner { get; set; }

    public virtual ICollection<InstallationDetail> InstallationDetails { get; set; } = new List<InstallationDetail>();

    public virtual ICollection<InstallationLocation> InstallationLocations { get; set; } = new List<InstallationLocation>();

    public virtual ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Permit> Permits { get; set; } = new List<Permit>();

    public virtual ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();

    public virtual ICollection<ProjectPhase> ProjectPhases { get; set; } = new List<ProjectPhase>();

    public virtual ICollection<ProjectStage> ProjectStages { get; set; } = new List<ProjectStage>();

    public virtual ICollection<SolarPanel> SolarPanels { get; set; } = new List<SolarPanel>();

    public virtual ProjectStatus? Status { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual Team? Team { get; set; }

    public virtual User? User { get; set; }
}
