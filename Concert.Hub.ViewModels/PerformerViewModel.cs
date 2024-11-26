using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class PerformerViewModel
	{
		public Guid Id { get; set; }

		[Required]
		[MaxLength(100)]
		[MinLength(3)]
		public string PerformerName { get; set; }
		public string StageName { get; set; }

		[Required]
		[MaxLength(500)]
		[MinLength(10)]
		public string Bio { get; set; }
	}
}
