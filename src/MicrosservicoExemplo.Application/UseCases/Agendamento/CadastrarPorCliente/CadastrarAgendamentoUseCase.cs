using System.Linq;
using Fleury.Agendamento.Domain.Agendamento.Repositorio;
using Fleury.Agendamento.Domain.Exame.Externo;
using Fleury.Agendamento.Domain.Paciente.Repositorio;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente
{
    public class CadastrarAgendamentoUseCase : ICadastrarAgendamentoUseCase
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IPacienteRepositorio _pacienteRepositorio;
        private readonly IExameServicoExterno _exameServicoExterno;

        public CadastrarAgendamentoUseCase(IAgendamentoRepositorio agendamentoRepositorio, IPacienteRepositorio pacienteRepositorio, IExameServicoExterno exameServicoExterno)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _pacienteRepositorio = pacienteRepositorio;
            _exameServicoExterno = exameServicoExterno;
        }

        public AgendamentoResult Cadastrar(AgendamentoRequest request)
        {
            var resultado = new AgendamentoResult();

            if (_pacienteRepositorio.Obter(request.Paciente.Cpf) == null)
            {
                resultado.AddNotification(nameof(request.Paciente.Cpf), "Paciente cadastrado");
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
                new Domain.Agendamento.Agendamento(request.Paciente, exames, request.DataAgendamento);

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
