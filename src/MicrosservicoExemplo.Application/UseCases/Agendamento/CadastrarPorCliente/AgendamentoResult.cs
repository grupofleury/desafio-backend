using System;
using System.Collections.Generic;
using System.Linq;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente
{
    public class AgendamentoResult : Result
    {
        public Guid Id { get; set; }

        public Domain.Paciente.Paciente Paciente { get; set; }

        public List<Domain.Exame.Exame> Exames { get; set; }

        public Decimal ValorTotalDeExames { get; set; }

        public static AgendamentoResult FromDomain(Domain.Agendamento.Agendamento agendamento)
        {
            return new AgendamentoResult
            {
               Id = agendamento.Id,
               Paciente = agendamento.Paciente,
               Exames = agendamento.Exames,
               ValorTotalDeExames = agendamento.Exames.Sum(e => e.Value)
            };
        }
    }
}
