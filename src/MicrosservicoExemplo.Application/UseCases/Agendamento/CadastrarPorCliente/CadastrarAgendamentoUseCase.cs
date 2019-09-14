using System.Linq;
using Fleury.Agendamento.Domain.Agendamento.Repositorio;
using Fleury.Agendamento.Domain.Cliente.Repositorio;
using Fleury.Agendamento.Domain.Exame.Externo;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente
{
    public class CadastrarAgendamentoUseCase : ICadastrarAgendamentoUseCase
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IExameServicoExterno _exameServicoExterno;

        public CadastrarAgendamentoUseCase(IAgendamentoRepositorio agendamentoRepositorio, IClienteRepositorio clienteRepositorio, IExameServicoExterno exameServicoExterno)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _exameServicoExterno = exameServicoExterno;
        }

        public AgendamentoResult Cadastrar(AgendamentoRequest request)
        {
            var resultado = new AgendamentoResult();

            if (_clienteRepositorio.Obter(request.Cliente.Cpf) == null)
            {
                resultado.AddNotification(nameof(request.Cliente.Cpf), "Cliente cadastrado");
                resultado.Error = ErrorCode.Business;
                return resultado;

            }

            if (request.Exames == null)
            {
                resultado.AddNotification(nameof(request.Exames), "Para realizar um agendamento é obrigatório informar ao menos um exame");
                resultado.Error = ErrorCode.Business;
                return resultado;
            }

            var ids = request.Exames.Select(x => x.Id);

            var exames = _exameServicoExterno.ObterExamesPorId(ids.ToList());

            var agendamento =
                new Domain.Agendamento.Agendamento(request.Cliente, exames, request.DataAgendamento);

            if (agendamento.Invalid)
            {
                resultado.AddNotifications(agendamento.Notifications);
                return resultado;
            }

            _agendamentoRepositorio.SalvarAgendamento(agendamento);
            return AgendamentoResult.FromDomain(agendamento);
        }
    }
}
