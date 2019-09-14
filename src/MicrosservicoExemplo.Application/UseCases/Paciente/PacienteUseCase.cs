using System.Linq;
using Fleury.Agendamento.Application.UseCases.Paciente.ListarPacientes;
using Fleury.Agendamento.Domain.Paciente.Repositorio;

namespace Fleury.Agendamento.Application.UseCases.Paciente
{
    public class PacienteUseCase : IPacienteUseCase
    {
        private readonly IPacienteRepositorio _pacienteRepositorio;

        public PacienteUseCase(IPacienteRepositorio pacienteRepositorio)
        {
            _pacienteRepositorio = pacienteRepositorio;
        }

        public PacienteResult Cadastrar(PacienteRequest request)
        {

            var resultado = ValidarPacienteRequest(request);

            if (resultado.Invalid)
                return resultado;

            var paciente =
                new Domain.Paciente.Paciente(request.Nome, request.Cpf, request.DataNascimento);

            if (paciente.Invalid)
            {
                resultado.AddNotifications(paciente.Notifications);
                return resultado;
            }

            paciente = _pacienteRepositorio.Salvar(paciente);

            if (paciente == null)
            {
                resultado.AddNotification("Paciente", "Paciente já cadastrado");
                resultado.Error = ErrorCode.Business;
                return resultado;
            }

            return PacienteResult.FromDomain(paciente);
        }

        private PacienteResult ValidarPacienteRequest(PacienteRequest request)
        {
            var resultado = new PacienteResult();
            if (request == null)
            {
                resultado.AddNotification("Request", "Requisição inválida");
                return resultado;
            }

            return resultado;
        }

        public PacienteResult Atualizar(PacienteRequest request)
        {
            var resultado = new PacienteResult();

            if (_pacienteRepositorio.Obter(request.Cpf) == null)
            {
                resultado.AddNotification(nameof(request.Cpf), "Paciente não cadastrado");
                resultado.Error = ErrorCode.NotFound;
                return resultado;
            }

            var cliente =
                new Domain.Paciente.Paciente(request.Nome, request.Cpf, request.DataNascimento);

            if (cliente.Invalid)
            {
                resultado.AddNotifications(cliente.Notifications);
                return resultado;
            }

            _pacienteRepositorio.Alterar(cliente);
            return PacienteResult.FromDomain(cliente);
        }

        public PacienteResult Excluir(string cpf)
        {
            var resultado = new PacienteResult();

            if (_pacienteRepositorio.Obter(cpf) == null)
            {
                resultado.AddNotification(nameof(cpf), "Paciente não cadastrado");
                resultado.Error = ErrorCode.NoContent;
                return resultado;
            }

            var cliente = _pacienteRepositorio.Excluir(cpf);
            return PacienteResult.FromDomain(cliente);
        }

        public ClienteListResult ObterTodos()
        {
            var resultado = new ClienteListResult();
            var clientes = _pacienteRepositorio.ObterClientes();
            if (!clientes.Any())
            {
                resultado.AddNotification("Nenhum Paciente não cadastrado");
                resultado.Error = ErrorCode.NoContent;
                return resultado;
            }
            resultado.FromListDomain(clientes.OrderBy(c => c.Nome).ToList());
            return resultado;
        }

        public PacienteResult ObterPorCpf(string cpf)
        {
            var resultado = new PacienteResult();
            var cliente = _pacienteRepositorio.Obter(cpf);
            if (cliente != null) return PacienteResult.FromDomain(cliente);
            resultado.AddNotification(nameof(cpf), "Paciente não cadastrado");
            resultado.Error = ErrorCode.NoContent;
            return resultado;
        }
    }
}
