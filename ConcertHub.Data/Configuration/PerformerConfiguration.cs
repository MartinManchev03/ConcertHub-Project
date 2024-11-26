using ConcertHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConcertHub.Common.EntityValidationConstraints.PerformerValidation;
namespace ConcertHub.Data.Configuration
{
    public class PerformerConfiguration : IEntityTypeConfiguration<Performer>
    {
        public void Configure(EntityTypeBuilder<Performer> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PerformerName)
                .IsRequired()
                .HasMaxLength(PerformerNameMaxLength);

            builder.Property(p => p.Bio)
                .HasMaxLength(BioMaxLength)
                .IsRequired();

            builder.HasOne(p => p.Creator)
                   .WithMany()
                   .HasForeignKey(p => p.CreatorId);
        }
    }
}
