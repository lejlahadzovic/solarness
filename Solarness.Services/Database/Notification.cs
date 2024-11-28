using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public int? ProjectId { get; set; }

    public string? Content { get; set; }

    public DateTime? SendDate { get; set; }

    public virtual Project? Project { get; set; }

    public virtual User? User { get; set; }
}
