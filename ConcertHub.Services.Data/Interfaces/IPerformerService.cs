﻿using ConcertHub.Data.Models;
using ConcertHub.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data.Interfaces
{
    public interface IPerformerService
    {
        IPagedList<AllPerformersViewModel> GetAllPerformers(int? page);
        Task AddPerformerAsync(PerformerViewModel viewModel, string creatorId);
        Task<DetailsPerformerViewModel> GetPerformerDetailsAsync(Guid id);
        Task<PerformerViewModel> GetPerformerForEditAsync(Guid id, string userId);
        Task EditPerformerAsync(PerformerViewModel viewModel);
        Task<DeleteViewModel> GetPerformerForDeleteAsync(Guid id, string userId);

        Task<DeleteViewModel> GetPerformerForDelete(Guid id, string userId);
        Task DeletePerformerAsync(Guid id);
    }
}
