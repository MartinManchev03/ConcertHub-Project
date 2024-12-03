using ConcertHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class ConcertPerformersViewModel
    {
        public Guid ConcertId { get; set; }

        public ICollection<PerformerConcertViewModel> PerformersNames { get; set; } = new List<PerformerConcertViewModel>();

        public string Organizer { get; set; }
    }
}
