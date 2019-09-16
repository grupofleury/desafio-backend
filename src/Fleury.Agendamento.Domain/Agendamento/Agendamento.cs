using System;
using System.Collections.Generic;
using Fleury.Agendamento.Domain.Exame;
using Flunt.Notifications;
using Flunt.Validations;

namespace Fleury.Agendamento.Domain.Agendamento
{
    public class Agendamento : Notifiable, IValidatable
    {
        public Guid Id { get; set; }

        public Paciente.Paciente Paciente { get; set; }

        public List<Exame.Exame> Exames { get; set; }

        public DateTime DataAgendamento { get; set; }

        public DateTime DataAlteracaoAgendamento { get; set; }

        public Decimal ValorTotalDeExames { get; set; }


        public Agendamento()
        {
            
        }

        public Agendamento(Paciente.Paciente paciente, DateTime dataAgendamento)
        {
            Paciente = paciente;
            DataAgendamento = dataAgendamento;
        }

        public Agendamento(Paciente.Paciente paciente, List<Exame.Exame> exames, DateTime dataAgendamento)
        {
            Id = Guid.NewGuid();
            Paciente = paciente;
            Exames = exames;
            DataAgendamento = dataAgendamento;
            Validate();
        }

        public Agendamento(Paciente.Paciente paciente, DateTime dataAgendamento, DateTime dataAlteracaoAgendamento)
        {
            Paciente = paciente;
            DataAgendamento = dataAgendamento;
            DataAlteracaoAgendamento = dataAlteracaoAgendamento;
            ValidarAlteracaoAgendamento();
        }

        public void Validate()
        {
            if (Valid)
            {
                AddNotifications(new Contract()
                    .IsNotNullOrEmpty(Paciente.Cpf, nameof(Paciente.Cpf), "Informe o cpf")
                    .IsNotNull(Exames, nameof(Exames),"Nenhum exame informado")
                    .IsGreaterThan(DataAgendamento, DateTime.Now, nameof(DataAgendamento), "Data agendamento deve ser maior que a data de hoje"));

            }
        }

        public void ValidarAlteracaoAgendamento()
        {
            if (Valid)
            {
                AddNotifications(new Contract()
                    .IsNotNullOrEmpty(Paciente.Cpf, nameof(Paciente.Cpf), "Informe o cpf")
                    .IsGreaterThan(DataAgendamento, DateTime.Now, nameof(DataAgendamento), "Data agendamento deve ser maior que a data de hoje"));

            }
        }
    }
}
