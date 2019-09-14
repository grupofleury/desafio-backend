using System;
using System.Collections.Generic;
using System.Linq;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.AlterarAgendamento
{
    public class AlterarAgendamentoResult : Result
    {
        public Guid Id { get; set; }

        public Domain.Paciente.Paciente Paciente { get; set; }

        public List<Domain.Exame.Exame> Exames { get; set; }

        public Decimal ValorTotalDeExames { get; set; }

        public DateTime DataAgendamento { get; set; }

        public static AlterarAgendamentoResult FromDomain(Domain.Agendamento.Agendamento agendamento)
        {
            return new AlterarAgendamentoResult
            {
                Id = agendamento.Id,
                DataAgendamento = agendamento.DataAgendamento,
                Paciente = agendamento.Paciente,
                Exames = agendamento.Exames,
                ValorTotalDeExames = agendamento.Exames.Sum(e => e.Value)
            };
        }
    }
}
