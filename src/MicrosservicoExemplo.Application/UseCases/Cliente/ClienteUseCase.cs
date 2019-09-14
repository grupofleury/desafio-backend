using System.Linq;
using Fleury.Agendamento.Application.UseCases.Cliente.ListarClientes;
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
                resultado.AddNotification(nameof(request.Cpf), "Cliente cadastrado");
                resultado.Error = ErrorCode.NotFound;
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
                resultado.AddNotification(nameof(request.Cpf), "Cliente não cadastrado");
                resultado.Error = ErrorCode.NotFound;
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

        public ClienteResult Excluir(string cpf)
        {
            var resultado = new ClienteResult();

            if (_clienteRepositorio.Obter(cpf) == null)
            {
                resultado.AddNotification(nameof(cpf), "Cliente não cadastrado");
                resultado.Error = ErrorCode.NoContent;
                return resultado;
            }

            var cliente = _clienteRepositorio.Excluir(cpf);
            return ClienteResult.FromDomain(cliente);
        }

        public ClienteListResult ObterTodos()
        {
            var resultado = new ClienteListResult();
            var clientes = _clienteRepositorio.ObterClientes();
            if (!clientes.Any())
            {
                resultado.AddNotification("Nenhum Cliente não cadastrado");
                resultado.Error = ErrorCode.NoContent;
                return resultado;
            }
            resultado.FromListDomain(clientes.OrderBy(c => c.Nome).ToList());
            return resultado;
        }

        public ClienteResult ObterPorCpf(string cpf)
        {
            var resultado = new ClienteResult();
            var cliente = _clienteRepositorio.Obter(cpf);
            if (cliente != null) return ClienteResult.FromDomain(cliente);
            resultado.AddNotification(nameof(cpf), "Cliente não cadastrado");
            resultado.Error = ErrorCode.NoContent;
            return resultado;
        }
    }
}
