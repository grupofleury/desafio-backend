using System;

namespace Fleury.Agendamento.Application.UseCases.Paciente
{
    public class PacienteResult : Result
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

       

        public static PacienteResult FromDomain(Domain.Paciente.Paciente paciente)
        {
            return new PacienteResult
            {
               Nome = paciente.Nome,
               Cpf = paciente.Cpf,
               DataNascimento = paciente.DataNascimento
            };
        }

       
    }
}
