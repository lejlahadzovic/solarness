using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class InstallationLocation
{
    public int LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public int? ProjectId { get; set; }

    public virtual ICollection<InstallationDetail> InstallationDetails { get; set; } = new List<InstallationDetail>();

    public virtual Project? Project { get; set; }
}
