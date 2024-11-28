using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarness.Model.Requests
{
    public class TeamMemberInsertRequest
    {
        public int? UserId { get; set; }

        public int? TeamId { get; set; }

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

        public virtual Team? Team { get; set; }

        public virtual User? User { get; set; }
    }
}
