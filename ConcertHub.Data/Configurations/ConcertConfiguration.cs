using ConcertHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConcertHub.Common.EntityValidationConstraints.ConcertValidation;
namespace ConcertHub.Data.Configurations
{
    public class ConcertConfiguration : IEntityTypeConfiguration<Concert>
    {
        public void Configure(EntityTypeBuilder<Concert> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ConcertName)
                .IsRequired()
                .HasMaxLength(ConcertNameMaxLength);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.Property(c => c.StartDate)
                .IsRequired();

            builder.Property(c => c.EndDate)
                .IsRequired();

            builder.Property(c => c.Location)
               .IsRequired()
               .HasMaxLength(LocationMaxLength);

            builder.HasOne(c => c.Organizer)
                .WithMany()
                .HasForeignKey(c => c.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
