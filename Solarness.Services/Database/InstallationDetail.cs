using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class InstallationDetail
{
    public int InstallationDetailId { get; set; }

    public int? LocationId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? InstallStartDate { get; set; }

    public DateOnly? InstallCompleteDate { get; set; }

    public string? InstallationType { get; set; }

    public int? NumberOfPanels { get; set; }

    public string? Description { get; set; }

    public int? ProjectId { get; set; }

    public virtual InstallationLocation? Location { get; set; }

    public virtual Project? Project { get; set; }

    public virtual User? User { get; set; }
}
