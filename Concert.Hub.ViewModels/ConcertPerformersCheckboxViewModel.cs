using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
	public class ConcertPerformersCheckboxViewModel
	{
		public Guid PerformerId { get; set; }
		public string PerformerName { get; set; }
		public bool IsChecked { get; set; } = false;
	}
}
