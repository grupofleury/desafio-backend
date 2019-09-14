using Fleury.Agendamento.Domain.Agendamento.Repositorio;
using Fleury.Agendamento.Domain.Paciente.Repositorio;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.ExcluirAgendamento
{
    public class ExcluirAgendamentoUseCase : IExcluirAgendamentoUseCase
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IPacienteRepositorio _pacienteRepositorio;

        public ExcluirAgendamentoUseCase(IAgendamentoRepositorio agendamentoRepositorio, IPacienteRepositorio pacienteRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _pacienteRepositorio = pacienteRepositorio;
        }

        public ExcluirAgendamentoResult Excluir(ExcluirAgendamentoRequest request)
        {
            var resultado = ValidarAgendamentoRequest(request);

            if (resultado.Invalid)
                return resultado;
         

            var agendamento =
                new Domain.Agendamento.Agendamento(request.Paciente, request.DataAgendamento);

            if (agendamento.Invalid)
            {
                resultado.AddNotifications(agendamento.Notifications);
                return resultado;
            }

            var agendamentoExcluido = _agendamentoRepositorio.ExcluirAgendamento(agendamento);

            if(!agendamentoExcluido)
            {
                resultado.AddNotification("Agendamento", "Não foi possível excluir o agendamento, verifique a data e horário");
                resultado.Error = ErrorCode.Business;
                return resultado;
            }


            resultado.AddNotification("Agendamento", "Agendamento excluído com sucesso");
            return resultado;
        }

        private ExcluirAgendamentoResult ValidarAgendamentoRequest(ExcluirAgendamentoRequest request)
        {
            var resultado = new ExcluirAgendamentoResult();
            if (request == null)
            {
                resultado.AddNotification("Request", "Requisição inválida");
                return resultado;
            }

            if (request.Paciente == null)
            {
                resultado.AddNotification("Request", "Paciente não informado");
                return resultado;
            }

            return resultado;
        }
    }
}
