using Fleury.Agendamento.Application.UseCases.Exames.ListarTodos;
using Fleury.Agendamento.Domain.Exame.Externo;
using Fleury.Agendamento.Infrastructure.Externo;
using Microsoft.Extensions.DependencyInjection;

namespace Fleury.Agendamento.Infrastructure.Bootstrap.Extensions.ServiceCollection
{
    public static class ExameServiceCollectionExtensions
    {
        public static void AddExames(this IServiceCollection services)
        {
            services.AddScoped<IExameServicoExterno, ExameServicoExterno>();
            services.AddScoped<IListarTodosUseCase, ListarTodosUseCase>();
        }
    }
}
