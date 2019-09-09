using Exam.Data.ContextConfig;
using Exam.Domain.Events;
using Exam.Domain.Interfaces.Repositories;
using Exam.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.EntityFrameworkCore.Extensions;
using Exam.ExternalServices.ExamRepository;

namespace Exam.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<DbContext, DataContext>();

            services.AddDbContext<DbContext>(opt => opt.UseMySQL(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();

            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IScheduleRepository, ScheduleRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IExamsRepository, ExamRepository>();

        }
    }
}
