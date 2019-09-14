using System;
using System.Collections.Generic;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorPaciente
{
    public class AgendamentoRequest
    {
        public Guid Id { get; set; }

        public Domain.Paciente.Paciente Paciente { get; set; }

        public List<Domain.Exame.Exame> Exames { get; set; }

        public DateTime DataAgendamento { get; set; }


    }
}
