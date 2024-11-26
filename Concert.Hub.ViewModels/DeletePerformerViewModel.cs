using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class DeletePerformerViewModel
    {
        public Guid Id { get; set; }

        public string PerformerName { get; set; }

        public string Creator { get; set; }
    }
}
