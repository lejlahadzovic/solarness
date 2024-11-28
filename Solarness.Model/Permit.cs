using System;
using System.Collections.Generic;

namespace Solarness.Model;

public partial class Permit
{
    public int PermitId { get; set; }

    public int? ProjectId { get; set; }

    public DateOnly? PermitSubmitDate { get; set; }

    public DateOnly? PermitReceivedDate { get; set; }

    public string? PermitType { get; set; }

    public string? Status { get; set; }

    public DateOnly? LastUpdatedDate { get; set; }

}
