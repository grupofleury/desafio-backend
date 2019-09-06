using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Data.EntityConfig
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("T001_CLNT");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("ID_CLNTE")
                .IsRequired();

            builder
                .HasIndex(x => new { x.Cpf})
                .IsUnique();
                
            builder
                .Property(x => x.Cpf)
                .HasColumnName("NUM_CPF")
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(x => x.DateBirth)
                .HasColumnName("DT_DATE");

            builder
                .Property(x => x.Name)
                .HasColumnName("DES_NOME")
                .IsRequired();
        }
    }
}
