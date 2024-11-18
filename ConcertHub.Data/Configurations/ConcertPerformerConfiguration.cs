using ConcertHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Configurations
{
    public class ConcertPerformerConfiguration : IEntityTypeConfiguration<ConcertPerformer>
    {
        public void Configure(EntityTypeBuilder<ConcertPerformer> builder)
        {
            builder.HasKey(cp => new { cp.PerformerId, cp.ConcertId });

            builder.HasOne(c => c.Concert)
                .WithMany(cp => cp.ConcertsPerformers)
                .HasForeignKey(c => c.ConcertId);

            builder.HasOne(p => p.Performer)
                .WithMany(cp => cp.ConcertsPerformers)
                .HasForeignKey(p => p.PerformerId);
        }
    }
}
