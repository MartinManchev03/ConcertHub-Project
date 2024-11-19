using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Models
{
    public class ConcertPerformer
    {
        public Guid ConcertId { get; set; } = Guid.NewGuid();

        public Concert Concert { get; set; }

        public Guid PerformerId { get; set; } = Guid.NewGuid();

        public Performer Performer { get; set; }


    }
}
