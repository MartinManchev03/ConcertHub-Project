using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class DeleteViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }

        public string ControllerName { get; set; }
    }
}
