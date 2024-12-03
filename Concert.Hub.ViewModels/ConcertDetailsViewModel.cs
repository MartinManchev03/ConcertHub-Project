using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class ConcertDetailsViewModel
    {
        public Guid Id { get; set; }

        public string ConcertName { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }
        public string OrganizerName { get; set; }

        public string CategoryName { get; set; }

        public ConcertPerformersViewModel ConcertPerformers { get; set; }

    }
}
