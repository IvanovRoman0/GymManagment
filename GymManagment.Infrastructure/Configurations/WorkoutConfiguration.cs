using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagment.Infrastructure.Configurations
{
    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.ToTable("workouts", "Gym");
            builder.HasKey(w => w.id);

            builder.Property(w => w.id)
                .ValueGeneratedOnAdd();

            builder.Property(w => w.ClientId)
                .HasColumnName("client_id")
                .IsRequired();

            builder.Property(w => w.WorkoutType)
                .HasColumnName("workout_type")
                .HasMaxLength(50);

            builder.Property(w => w.Duration)
                .HasColumnName("duration");

            builder.Property(w => w.DateTime)
                .HasColumnName("date_time")
                .HasColumnType("timestamp");

            builder.Property(w => w.GymId)
                .HasColumnName("gym_id")
                .IsRequired();

            builder.HasOne(w => w.Client)
                .WithMany()
                .HasForeignKey(w => w.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.Gym)
                .WithMany()
                .HasForeignKey(w => w.GymId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
