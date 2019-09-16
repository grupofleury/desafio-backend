using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace Fleury.Agendamento.Domain.Paciente
{
    public class Paciente : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }


        public Paciente()
        {
          
        }

        public Paciente(string nome, string cpf, DateTime datanascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = datanascimento;
            Validate();

        }

        public Paciente(string cpf)
        {
            Cpf = cpf;
        }

        public Paciente(string nome, DateTime datanascimento)
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
                    .IsNotNullOrEmpty(Nome, nameof(Nome), "Informe o nome do paciente")
                    .IsLowerThan(DataNascimento, DateTime.Now,  nameof(DataNascimento),"Data de nascimento deve ser menor que a data de hoje")
                    .IsBetween(DataNascimento, new DateTime(1900,01,01), DateTime.Now, nameof(DataNascimento),"Data de nascimento inválida" )
                    .IsNotNullOrEmpty(Cpf, nameof(Cpf), "Informe o Cpf"));
            }
        }

        public void ValidarAlteracao()
        {
            if (Valid)
            {
                AddNotifications(new Contract()
                    .IsNotNullOrEmpty(Nome, nameof(Nome), "Informe o nome do paciente"));

            }
        }
    }
}
