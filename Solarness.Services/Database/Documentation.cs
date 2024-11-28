using System;
using System.Collections.Generic;

namespace Solarness.Services.Database;

public partial class Documentation
{
    public int DocumentId { get; set; }

    public string DocumentName { get; set; } = null!;

    public string? DocumentType { get; set; }

    public string? DocumentLocation { get; set; }

    public int? ProjectId { get; set; }

    public DateOnly? AdditionDate { get; set; }

    public virtual Project? Project { get; set; }
}
