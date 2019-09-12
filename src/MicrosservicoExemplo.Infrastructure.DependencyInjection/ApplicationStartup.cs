using Fleury.Agendamento.Infrastructure.Bootstrap.Extensions.ApplicationBuilder;
using Fleury.Agendamento.Infrastructure.Bootstrap.Extensions.ServiceCollection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fleury.Agendamento.Infrastructure.Bootstrap
{
    public class ApplicationStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddClientes();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddAgendamentoHealthChecks();
            services.AddAGendamentoSwagger();
            services.AddAgendamentoResponseCompression();
            services.AddMetrics();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAgendamentoSwagger();
            app.UseAgendamentoHealthChecks();
            app.UseResponseCompression();
            app.UseMvc();
        }
    }
}
