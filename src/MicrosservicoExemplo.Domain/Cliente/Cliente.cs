using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace Fleury.Agendamento.Domain.Cliente
{
    public class Cliente : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }


        public Cliente()
        {
          
        }

        public Cliente(string nome, string cpf, DateTime datanascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = datanascimento;
            Validate();

        }

        public Cliente(string cpf)
        {
            Cpf = cpf;
        }

        public Cliente(string nome, DateTime datanascimento)
        {
            Nome = nome;
            DataNascimento = datanascimento;
            ValidarAlteracao();
        }



        public void Validate()
        {
            if (Valid)
            {
                AddNotifications(new Contract()
                    .IsNotNullOrEmpty(Nome, nameof(Nome), "Informe o nome do cliente")
                    .IsNotNullOrEmpty(Cpf, nameof(Cpf), "Informe o Cpf"));
            }
        }

        public void ValidarAlteracao()
        {
            if (Valid)
            {
                AddNotifications(new Contract()
                    .IsNotNullOrEmpty(Nome, nameof(Nome), "Informe o nome do cliente"));

            }
        }
    }
}
