using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Fleury.Agendamento.Infrastructure.Bootstrap.Extensions.ServiceCollection
{
    public static class SwaggerServiceCollectionExtensions
    {

        public static void AddAgendamentoSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Agendamento API",
                        Version = "v1"
                    }
                 );

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var filePath = Path.Combine(basePath, xmlFile);
                config.IncludeXmlComments(filePath);
            });
        }
    }
}
