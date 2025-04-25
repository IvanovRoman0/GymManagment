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
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("class", "Gym");

            builder.Property(c => c.id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.TrainerId)
                .HasColumnName("trainer_id");

            builder.Property(c => c.GymId)
                .HasColumnName("gym_id");

            builder.Property(c => c.ClassName)
                .HasColumnName("class_name")
                .HasMaxLength(100);

            builder.Property(c => c.ClassType)
                .HasColumnName("class_type")
                .HasMaxLength(20)
                .HasConversion<string>();

            builder.Property(c => c.DateTime)
                .HasColumnName("date_time")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
