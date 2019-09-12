using Fleury.Agendamento.Application.UseCases.Cliente;
using Fleury.Agendamento.Domain.Cliente.Repositorio;
using Fleury.Agendamento.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Fleury.Agendamento.Infrastructure.Bootstrap.Extensions.ServiceCollection
{
    public static class ClienteServiceCollectionExtensions
    {
        public static void AddClientes(this IServiceCollection services)
        {
            services.AddSingleton<IClienteRepositorio, InMemoryClientRepository>();
            services.AddScoped<IClienteUseCase, ClienteUseCase>();
        }
    }
}
