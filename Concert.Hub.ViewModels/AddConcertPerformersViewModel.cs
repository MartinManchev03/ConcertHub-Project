using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
	public class AddConcertPerformersViewModel
	{
		public Guid ConcertId { get; set; }

		public List<ConcertPerformersCheckboxViewModel> ConcertPerformers { get; set; } = new List<ConcertPerformersCheckboxViewModel>();

	}
}
