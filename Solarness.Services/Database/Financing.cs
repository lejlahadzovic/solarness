using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class Financing
{
    public int FinancingId { get; set; }

    public int? ProjectId { get; set; }

    public string? FinancingName { get; set; }

    public decimal? FinancingAmount { get; set; }

    public DateOnly? FinancingDate { get; set; }

    public virtual Project? Project { get; set; }
}
