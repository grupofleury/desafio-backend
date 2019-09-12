using Fleury.Agendamento.Domain.Cliente.Repositorio;

namespace Fleury.Agendamento.Application.UseCases.Cliente.CadastrarCliente
{
    public class CadastrarClienteUseCase : ICadastrarClienteUseCase
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public CadastrarClienteUseCase(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public CadastrarClienteResult Executar(CadastrarClienteRequest request)
        {
            var resultado = new CadastrarClienteResult();

            if (_clienteRepositorio.Obter(request.Cpf) != null)
            {
                resultado.AddNotification(nameof(request.Cpf), "Cliente cadastrada");
                return resultado;
            }

            Domain.Cliente.Cliente conta = new Domain.Cliente.Cliente();

            if (conta.Invalid)
            {
                resultado.AddNotifications(conta.Notifications);
                return resultado;
            }

            _clienteRepositorio.Salvar(conta);
            return CadastrarClienteResult.FromDomain(conta);
        }
    }
}
