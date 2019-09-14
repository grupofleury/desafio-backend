using System;
using System.Collections.Generic;
using System.Text;
using Fleury.Agendamento.Infrastructure.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace Fleury.Agendamento.Infrastructure.Bootstrap.Extensions.ServiceCollection
{
    public static class FleuryRestClientCollectionExtensions
    {
        public static void AddFleuryRestClient(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRestClient, FleuryRestClient>();
        }
    }
}
