using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarness.Model.Requests
{
    public class PermitUpdateRequest
    {
        public int? ProjectId { get; set; }

        public DateOnly? PermitSubmitDate { get; set; }

        public DateOnly? PermitReceivedDate { get; set; }

        public string? PermitType { get; set; }

        public string? Status { get; set; }

        public DateOnly? LastUpdatedDate { get; set; }
    }
}
