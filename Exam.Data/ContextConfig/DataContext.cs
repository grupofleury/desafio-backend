using Exam.Domain.Entities;
using Exam.Domain.Interfaces.Repositories;
using Exam.Data.EntityConfig;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam.Data.ContextConfig
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DbContext> options) : base(options) { }            

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());

            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }
}

