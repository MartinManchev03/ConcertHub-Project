using ConcertHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Configuration
{
    public class ConcertPerformerConfiguration : IEntityTypeConfiguration<ConcertPerformer>
    {
        public void Configure(EntityTypeBuilder<ConcertPerformer> builder)
        {
            builder.HasKey(cp => new { cp.ConcertId, cp.PerformerId});

            builder.HasOne(c => c.Concert)
                .WithMany(cp => cp.ConcertPerformers)
                .HasForeignKey(c => c.ConcertId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Performer)
                .WithMany(cp => cp.ConcertPerformers)
                .HasForeignKey(p => p.PerformerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
