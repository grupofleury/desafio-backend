using Fleury.Agendamento.Application.UseCases.Paciente;
using Fleury.Agendamento.Domain.Paciente.Repositorio;
using Fleury.Agendamento.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Fleury.Agendamento.Infrastructure.Bootstrap.Extensions.ServiceCollection
{
    public static class ClienteServiceCollectionExtensions
    {
        public static void AddClientes(this IServiceCollection services)
        {
            services.AddSingleton<IPacienteRepositorio, MemoriaPacienteRepositorio>();
            services.AddScoped<IPacienteUseCase, PacienteUseCase>();
        }
    }
}
