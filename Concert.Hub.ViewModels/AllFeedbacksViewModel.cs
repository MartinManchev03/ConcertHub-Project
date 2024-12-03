using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class AllFeedbacksViewModel
    {
        public Guid Id { get; set; }
        public string PostedBy { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public string ConcertOrganizer { get; set; }

    }
}
