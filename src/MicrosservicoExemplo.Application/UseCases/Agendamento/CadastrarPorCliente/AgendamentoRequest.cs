using System;
using System.Collections.Generic;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente
{
    public class AgendamentoRequest
    {
        public Guid Id { get; set; }

        public Domain.Cliente.Cliente Cliente { get; set; }

        public List<Domain.Exame.Exame> Exames { get; set; }

        public DateTime DataAgendamento { get; set; }


    }
}
