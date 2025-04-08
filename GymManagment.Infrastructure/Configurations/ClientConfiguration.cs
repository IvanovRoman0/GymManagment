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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients", "Gym");
            builder.Property(c => c.Id)
                .HasColumnName("id")
                .HasColumnType("integer");
            builder.Property(c => c.FirstName)
                .HasColumnName("first_name")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.PhoneNumber)
                .HasColumnName("phone_number")
                .IsRequired()
                .HasMaxLength(11);
            builder.Property(c => c.Gender)
                .HasColumnName("gender")
                .HasConversion<string>()
                .HasMaxLength(11);
            builder.Property(c => c.DateOfBirth)
                .HasColumnName("date_of_birth")
                .HasColumnType("date");
            builder.Property(c => c.RegistrationDate)
                .HasColumnName("registration_date")
                .HasColumnType("date");

        }
    }
}
