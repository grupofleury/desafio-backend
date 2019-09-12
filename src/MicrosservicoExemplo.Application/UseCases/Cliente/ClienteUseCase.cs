using Fleury.Agendamento.Domain.Cliente.Repositorio;

namespace Fleury.Agendamento.Application.UseCases.Cliente
{
    public class ClienteUseCase : IClienteUseCase
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteUseCase(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public ClienteResult Cadastrar(ClienteRequest request)
        {
            var resultado = new ClienteResult();

            if (_clienteRepositorio.Obter(request.Cpf) != null)
            {
                resultado.AddNotification(nameof(request.Cpf), "Cliente cadastrada");
                return resultado;
            }

            var cliente =
                new Domain.Cliente.Cliente(request.Nome, request.Cpf, request.DataNascimento);

            if (cliente.Invalid)
            {
                resultado.AddNotifications(cliente.Notifications);
                return resultado;
            }

            _clienteRepositorio.Salvar(cliente);
            return ClienteResult.FromDomain(cliente);
        }

        public ClienteResult Atualizar(ClienteRequest request)
        {
            var resultado = new ClienteResult();

            if (_clienteRepositorio.Obter(request.Cpf) == null)
            {
                resultado.AddNotification(nameof(request.Cpf), "Cliente não cadastrada");
                return resultado;
            }

            var cliente =
                new Domain.Cliente.Cliente(request.Nome, request.Cpf, request.DataNascimento);

            if (cliente.Invalid)
            {
                resultado.AddNotifications(cliente.Notifications);
                return resultado;
            }

            _clienteRepositorio.Atualizar(cliente);
            return ClienteResult.FromDomain(cliente);
        }

        public ClienteResult Excluir(string cpf)
        {
            throw new System.NotImplementedException();
        }

        public ClienteResult ObterTodos()
        {
            throw new System.NotImplementedException();
        }
    }
}
