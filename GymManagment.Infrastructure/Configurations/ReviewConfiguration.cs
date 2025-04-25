using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GymManagment.Infrastructure.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Reviews>
    {
        public void Configure(EntityTypeBuilder<Reviews> builder)
        {
            builder.ToTable("reviews", "Gym");

            builder.Property(r => r.id);
            builder.Property(r => r.ClientId)
                .HasColumnName("client_id");
            builder.Property(r => r.TrainerId)
                .HasColumnName("trainer_id");
            builder.Property(r => r.GymId)
                .HasColumnName("gym_id");
            builder.Property(r => r.Raiting)
                .HasColumnName("raiting");
            builder.Property(r => r.Comment)
                .HasColumnName("comment");
            builder.Property(r => r.ReviewDate)
                .HasColumnName("review_date")
                .HasColumnType("date");

            builder.HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId);
            builder.HasOne(r => r.Trainer)
                .WithMany()
                .HasForeignKey(r => r.TrainerId);
            builder.HasOne(r => r.Gym)
                .WithMany()
                .HasForeignKey(r => r.GymId);
        }
    }
}
