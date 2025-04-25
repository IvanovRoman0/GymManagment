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
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.ToTable("specialization", "Gym");
            builder.Property(s => s.id)
                .ValueGeneratedOnAdd();
            builder.Property(s => s.SpecializationName)
                .HasColumnName("specialization_name")
                .HasMaxLength(100);
        }
    }
}
