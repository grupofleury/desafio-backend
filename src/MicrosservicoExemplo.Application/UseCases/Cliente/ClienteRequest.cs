using System;

namespace Fleury.Agendamento.Application.UseCases.Cliente
{
    public class ClienteRequest
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}