using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class ProjectPhase
{
    public int PhaseId { get; set; }

    public string PhaseName { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? ProjectId { get; set; }

    public string? Status { get; set; }

    public virtual Project? Project { get; set; }
}
