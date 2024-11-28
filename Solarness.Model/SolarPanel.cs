using System;
using System.Collections.Generic;

namespace Solarness.Model;

public partial class SolarPanel
{
    public int PanelId { get; set; }

    public string ModelName { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public int? ProjectId { get; set; }

    public DateOnly? InstallationDate { get; set; }

    public double? Efficiency { get; set; }

    public double? EnergyProduced { get; set; }

    public virtual ICollection<PerformanceHistory> PerformanceHistories { get; set; } = new List<PerformanceHistory>();

    public virtual Project? Project { get; set; }
}
