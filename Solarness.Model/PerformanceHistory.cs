using System;
using System.Collections.Generic;

namespace Solarness.Model;

public partial class PerformanceHistory
{
    public int HistoryId { get; set; }

    public int? PanelId { get; set; }

    public DateOnly? Date { get; set; }

    public double? Efficiency { get; set; }

    public double? EnergyProduced { get; set; }

    public virtual SolarPanel? Panel { get; set; }
}
