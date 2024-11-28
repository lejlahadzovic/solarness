using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarness.Model.Requests
{
    public class UserUpdateRequest
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Picture { get; set; }

        public string PasswordHash { get; set; } = null!;

        public string PasswordSalt { get; set; } = null!;

        public int RoleId { get; set; }
    }
}
