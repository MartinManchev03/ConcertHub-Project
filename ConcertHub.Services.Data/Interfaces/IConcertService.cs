using ConcertHub.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data.Interfaces
{
    public interface IConcertService
    {
        IPagedList<AllConcertsViewModel> GetAllConcerts(int? page);

    }
}
