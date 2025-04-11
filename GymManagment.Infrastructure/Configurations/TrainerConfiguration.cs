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
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.ToTable("trainers", "Gym");
            builder.Property(t => t.Id)
                .HasColumnName("id")
                .HasColumnType("integer");
            builder.Property(t => t.FirstName)
                .HasColumnName("first_name")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(t => t.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(t => t.PhoneNumber)
                .HasColumnName("phone_number")
                .IsRequired()
                .HasMaxLength(11);
            builder.Property(t => t.Email)
                .HasColumnName("email")
                .HasMaxLength(100);
            builder.Property(t => t.SpecializationId)
                .HasColumnName("specialization_id")
                .HasColumnType("integer");
            builder.HasOne(t => t.Specialization)
                .WithMany()
                .HasForeignKey(t => t.SpecializationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
