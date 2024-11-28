using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class Milestone
{
    public int MilestoneId { get; set; }

    public int? ProjectId { get; set; }

    public string? MilestoneName { get; set; }

    public DateOnly? MilestoneDate { get; set; }

    public string? Description { get; set; }

    public virtual Project? Project { get; set; }
}
