using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data
{
    public class ConcertService : IConcertService
    {
        private readonly IRepository<Concert, Guid> concertRepository;

        public ConcertService(IRepository<Concert, Guid> concertRepository)
        {
            this.concertRepository = concertRepository;
        }

        public IPagedList<AllConcertsViewModel> GetAllConcerts(int? page)
        {
            throw new NotImplementedException();
        }
    }
}
