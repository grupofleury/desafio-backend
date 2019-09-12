using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace Fleury.Agendamento.Domain.Cliente
{
    public class Cliente : Notifiable, IValidatable
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract());
        }

        public void ValidarCliente(string numero)
        {
            if (Valid)
            {
                AddNotifications(new Contract()
                    .IsNotNullOrEmpty(numero, nameof(Cpf), "Informe o Cpf"));
            }
        }
    }
}
