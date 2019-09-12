using System;

namespace Fleury.Agendamento.Application.UseCases.Cliente.CadastrarCliente
{
    public class CadastrarClienteRequest
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}