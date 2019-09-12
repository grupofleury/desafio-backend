using System;

namespace Fleury.Agendamento.Application.UseCases.Cliente
{
    public class ClienteResult : Result
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }


        public static ClienteResult FromDomain(Domain.Cliente.Cliente cliente)
        {
            return new ClienteResult
            {
               Nome = cliente.Nome,
               Cpf = cliente.Cpf,
               DataNascimento = cliente.DataNascimento
            };
        }
    }
}
