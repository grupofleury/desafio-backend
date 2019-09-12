using System;

namespace Fleury.Agendamento.Application.UseCases.Cliente.CadastrarCliente
{
    public class CadastrarClienteResult : Result
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }


        public static CadastrarClienteResult FromDomain(Domain.Cliente.Cliente cliente)
        {
            return new CadastrarClienteResult
            {
                
               
            };
        }
    }
}
