using ConcertHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConcertHub.Common.EntityValidationConstraints.FeedBackValidation;
namespace ConcertHub.Data.Configurations
{
    public class FeedBackConfiguration : IEntityTypeConfiguration<FeedBack>
    {
        public void Configure(EntityTypeBuilder<FeedBack> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Rating)
                .IsRequired()
                .HasMaxLength(RatingMax);

            builder.Property(f => f.Comment)
                .HasMaxLength(CommentMaxLength);

            builder.HasOne(f => f.PostedBy)
                .WithMany()
                .HasForeignKey(f => f.PostedById);

            builder.HasOne(f => f.Concert)
                .WithMany()
                .HasForeignKey(f => f.ConcertId);
        }
    }
}
