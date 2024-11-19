﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Models
{
    public class Performer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string PerformerName { get; set; }

        public string StageName { get; set; }

        public string Bio { get; set; }

        public string CreatorId { get; set; }

        public IdentityUser Creator { get; set; }

        public ICollection<ConcertPerformer> ConcertPerformers { get; set; } = new HashSet<ConcertPerformer>();

    }
}
