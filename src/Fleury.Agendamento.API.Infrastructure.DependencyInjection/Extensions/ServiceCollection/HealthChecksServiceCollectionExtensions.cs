using Microsoft.Extensions.DependencyInjection;

namespace Fleury.Agendamento.Infrastructure.Bootstrap.Extensions.ServiceCollection
{
    public static class HealthChecksServiceCollectionExtensions
    {
        public static void AddAgendamentoHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();
        }
    }
}
