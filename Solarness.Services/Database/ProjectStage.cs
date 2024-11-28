using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class ProjectStage
{
    public int StageId { get; set; }

    public int? ProjectId { get; set; }

    public string? StageName { get; set; }

    public string? StageGroup { get; set; }

    public DateOnly? StageStartDate { get; set; }

    public DateOnly? StageEndDate { get; set; }

    public int? DaysInStage { get; set; }

    public DateOnly? StageUpdatedDate { get; set; }

    public virtual Project? Project { get; set; }
}
