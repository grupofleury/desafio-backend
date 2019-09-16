using System.Linq;
using Fleury.Agendamento.Domain.Agendamento.Repositorio;
using Fleury.Agendamento.Domain.Exame.Externo;
using Fleury.Agendamento.Domain.Paciente.Repositorio;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorPaciente
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
            var resultado = ValidarAgendamentoRequest(request);

            if (resultado.Invalid)
                return resultado;

            if (_pacienteRepositorio.Obter(request.Paciente.Cpf) == null)
            {
                resultado.AddNotification(nameof(request.Paciente.Cpf), "Paciente não cadastrado");
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

            agendamento = _agendamentoRepositorio.SalvarAgendamento(agendamento);

            if (agendamento == null)
            {
                resultado.AddNotification("Agendamento", "Não foi possível realizar o agendamento, selecione outra data e horário");
                resultado.Error = ErrorCode.Business;
                return resultado;
            }


            return AgendamentoResult.FromDomain(agendamento);
        }

        private AgendamentoResult ValidarAgendamentoRequest(AgendamentoRequest request)
        {
            var resultado = new AgendamentoResult();
            if (request == null)
            {
                resultado.AddNotification("Request", "Requisição inválida");
                return resultado;
            }

            return resultado;
        }
    }
}
