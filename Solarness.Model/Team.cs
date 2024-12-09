using System;
using System.Collections.Generic;

namespace Solarness.Model;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
