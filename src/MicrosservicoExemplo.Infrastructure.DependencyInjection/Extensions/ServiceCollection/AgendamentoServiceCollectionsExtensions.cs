using Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente;
using Fleury.Agendamento.Application.UseCases.Agendamento.ListarPorCliente;
using Fleury.Agendamento.Domain.Agendamento.Repositorio;
using Fleury.Agendamento.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Fleury.Agendamento.Infrastructure.Bootstrap.Extensions.ServiceCollection
{
    public static class AgendamentoServiceCollectionsExtensions
    {
        public static void AddAgendamentos(this IServiceCollection services)
        {
            services.AddSingleton<IAgendamentoRepositorio, MemoriaAgendamentoRepositorio>();
            services.AddScoped<ICadastrarAgendamentoUseCase, CadastrarAgendamentoUseCase>();
            services.AddScoped<IListarAgendamentoPorClienteUseCase, ListarAgendamentoPorClienteUseCase>();
        }
    }
}
