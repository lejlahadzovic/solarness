﻿using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class ProjectStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
