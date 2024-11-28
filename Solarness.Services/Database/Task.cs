using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class Task
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Status { get; set; }

    public int? ProjectId { get; set; }

    public int? MemberId { get; set; }

    public virtual TeamMember? Member { get; set; }

    public virtual Project? Project { get; set; }
}
