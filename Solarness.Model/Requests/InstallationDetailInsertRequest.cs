using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarness.Model.Requests
{
    public class InstallationDetailInsertRequest
    {
        public int? LocationId { get; set; }

        public int? UserId { get; set; }

        public DateOnly? InstallStartDate { get; set; }

        public DateOnly? InstallCompleteDate { get; set; }

        public string? InstallationType { get; set; }

        public int? NumberOfPanels { get; set; }

        public string? Description { get; set; }

        public int? ProjectId { get; set; }
    }
}
