﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarness.Model.Requests
{
    public class InstallationLocationInsertRequest
    {
        public string LocationName { get; set; } = null!;

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int? ProjectId { get; set; }
    }
}
