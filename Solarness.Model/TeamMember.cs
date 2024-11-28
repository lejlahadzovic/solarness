using System;
using System.Collections.Generic;

namespace Solarness.Model;

public partial class TeamMember
{
    public int MemberId { get; set; }

    public int? UserId { get; set; }

    public int? TeamId { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual Team? Team { get; set; }

    public virtual User? User { get; set; }
}
