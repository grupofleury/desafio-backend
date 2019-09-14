using System;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.AlterarAgendamento
{
    public class AlterarAgendamentoRequest
    {
        public Domain.Paciente.Paciente Paciente { get; set; }
        public DateTime DataAgendamento { get; set; }

        public DateTime DataAlteracaoAgendamento { get; set; }


    }
}
