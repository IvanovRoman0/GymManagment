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
    public class RegistrationClassConfiguration : IEntityTypeConfiguration<RegistrationClass>
    {
        public void Configure(EntityTypeBuilder<RegistrationClass> builder)
        {
            builder.ToTable("registrations_classes", "Gym");
            builder.HasKey(rc => rc.Id);

            builder.Property(rc => rc.Id)
                .HasColumnName("id")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.Property(rc => rc.ClientId)
                .HasColumnName("client_id")
                .IsRequired();

            builder.Property(rc => rc.RegistrationDate)
                .HasColumnName("registration_date")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(rc => rc.ClassId)
                .HasColumnName("class_id")
                .IsRequired();

            builder.HasOne(rc => rc.Client)
                .WithMany()
                .HasForeignKey(rc => rc.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rc => rc.Class)
                .WithMany()
                .HasForeignKey(rc => rc.ClassId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
