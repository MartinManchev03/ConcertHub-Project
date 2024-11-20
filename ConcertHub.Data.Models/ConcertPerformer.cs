using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Models
{
    public class ConcertPerformer
    {
        public ConcertPerformer()
        {
            ConcertId = Guid.NewGuid();
            PerformerId = Guid.NewGuid();
        }
        public Guid ConcertId { get; set; }

        public Concert Concert { get; set; }

        public Guid PerformerId { get; set; }

        public Performer Performer { get; set; }


    }
}
