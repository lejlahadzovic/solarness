using System;
using System.Collections.Generic;

namespace Solarness.Model;

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

    public virtual ProjectStatus? Status { get; set; }

    public virtual Team? Team { get; set; }
}