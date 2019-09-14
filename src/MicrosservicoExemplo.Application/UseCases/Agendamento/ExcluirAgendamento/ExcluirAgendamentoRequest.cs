using System;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.ExcluirAgendamento
{
    public class ExcluirAgendamentoRequest
    {
        public Domain.Paciente.Paciente Paciente { get; set; }
        public DateTime DataAgendamento { get; set; }
    }
}
