 using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class Prediction
{
    public int PredictionId { get; set; }

    public int? ProjectId { get; set; }

    public DateOnly? PredictionDate { get; set; }

    public string? PredictionStatus { get; set; }

    public double? Confidence { get; set; }

    public virtual Project? Project { get; set; }
}
