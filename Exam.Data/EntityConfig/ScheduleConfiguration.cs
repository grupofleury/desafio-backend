using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Data.EntityConfig
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("T002_SCHL");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("ID_SCHL")
                .IsRequired();

            builder
                .Property(x => x.IdExam)
                .HasColumnName("ID_EXME")
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasColumnName("NOME_EXME")
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(x => x.DateSchedule)
                .HasColumnName("DT_DATE");

            builder
                .Property(x => x.Value)
                .HasColumnName("NUM_VALOR")
                .IsRequired();

            builder
                .Property(x => x.ClientId)
                .HasColumnName("ID_CLNTE")
                .IsRequired();

            builder.HasOne(x => x.Client)
                .WithMany(x => x.Schedules)
                .HasForeignKey(x => new { x.ClientId });
        }
    }
}