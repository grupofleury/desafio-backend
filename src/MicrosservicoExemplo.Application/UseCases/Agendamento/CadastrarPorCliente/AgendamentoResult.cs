using System;
using System.Collections.Generic;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente
{
    public class AgendamentoResult : Result
    {
        public Guid Id { get; set; }

        public Domain.Cliente.Cliente Cliente { get; set; }

        public List<Domain.Exame.ExameDto> Exames { get; set; }

        public static AgendamentoResult FromDomain(Domain.Agendamento.Agendamento agendamento)
        {
            return new AgendamentoResult
            {
               Id = agendamento.Id,
               Cliente = agendamento.Cliente,
               Exames = agendamento.Exames
            };
        }
    }
}
