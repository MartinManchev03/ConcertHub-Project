using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Models
{
    public class ConcertPerformer
    {
        public int ConcertId { get; set; }

        public Concert Concert { get; set; } = null!;

        public int PerformerId { get; set; }

        public Performer Performer { get; set; } = null!;


    }
}
