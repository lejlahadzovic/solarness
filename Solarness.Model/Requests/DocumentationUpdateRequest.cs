using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarness.Model.Requests
{
    public class DocumentationUpdateRequest
    {
        public string DocumentName { get; set; } = null!;

        public string? DocumentType { get; set; }

        public string? DocumentLocation { get; set; }

        public int? ProjectId { get; set; }

        public DateOnly? AdditionDate { get; set; }
    }
}
