using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Models
{
    public class Performer
    {
        public Performer()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public string PerformerName { get; set; }

        public string StageName { get; set; }

        public string BioP { get; set; }

        public string CreatorId { get; set; }

        public IdentityUser Creator { get; set; }

        public ICollection<ConcertPerformer> ConcertsPerformers { get; set; } = new HashSet<ConcertPerformer>();

    }
}
