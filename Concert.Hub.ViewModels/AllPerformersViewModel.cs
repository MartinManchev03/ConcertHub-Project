using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class AllPerformersViewModel
    {
        public Guid Id { get; set; }
        public string PerformerName { get; set; }
        public string StageName { get; set; }

        public string Creator { get; set; }
    }
}
