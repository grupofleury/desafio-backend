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

        public Cliente.Cliente Cliente { get; set; }

        public List<Exame.ExameDto> Exames { get; set; }

        public DateTime DataAgendamento { get; set; }

        public Decimal Valor { get; set; }


        public Agendamento()
        {
            
        }

        public Agendamento(Cliente.Cliente cliente, List<ExameDto> exames, DateTime dataAgendamento)
        {
            Id = Guid.NewGuid();
            Cliente = cliente;
            Exames = exames;
            DataAgendamento = dataAgendamento;
        }

        public void Validate()
        {
            if (Valid)
            {
                AddNotifications(new Contract()
                    
                    .IsNotNullOrEmpty(Cliente.Cpf, nameof(Cliente.Cpf), "Informe o cpf"));

            }
        }
    }
}
