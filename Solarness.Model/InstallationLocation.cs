using System;
using System.Collections.Generic;

namespace Solarness.Model;

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
}
