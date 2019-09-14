using Fleury.Agendamento.Domain.Agendamento.Repositorio;
using Fleury.Agendamento.Domain.Paciente.Repositorio;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.AlterarAgendamento
{
    public class AlterarAgendamentoUseCase : IAlterarAgendamentoUseCase
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IPacienteRepositorio _pacienteRepositorio;

        public AlterarAgendamentoUseCase(IAgendamentoRepositorio agendamentoRepositorio, IPacienteRepositorio pacienteRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _pacienteRepositorio = pacienteRepositorio;
        }

        public AlterarAgendamentoResult Alterar(AlterarAgendamentoRequest request)
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

            var agendamento =
                new Domain.Agendamento.Agendamento(request.Paciente, request.DataAgendamento, request.DataAlteracaoAgendamento);

            if (agendamento.Invalid)
            {
                resultado.AddNotifications(agendamento.Notifications);
                return resultado;
            }

            agendamento = _agendamentoRepositorio.AlterarAgendamento(agendamento);

            if (agendamento == null)
            {
                resultado.AddNotification("Agendamento", "Não foi possível alterar o agendamento, selecione outra data e horário");
                resultado.Error = ErrorCode.Business;
                return resultado;
            }


            return AlterarAgendamentoResult.FromDomain(agendamento);
        }

        private AlterarAgendamentoResult ValidarAgendamentoRequest(AlterarAgendamentoRequest request)
        {
            var resultado = new AlterarAgendamentoResult();
            if (request == null)
            {
                resultado.AddNotification("Request", "Requisição inválida");
                return resultado;
            }

            return resultado;
        }
    }
}
