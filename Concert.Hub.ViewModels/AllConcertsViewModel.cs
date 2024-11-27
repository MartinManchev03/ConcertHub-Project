using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class AllConcertsViewModel
    {
        public Guid Id { get; set; }

        public string ConcertName { get; set; }

        public string Category { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public string Organizer { get; set; }

        public bool IsJoined { get; set; }

    }
}
