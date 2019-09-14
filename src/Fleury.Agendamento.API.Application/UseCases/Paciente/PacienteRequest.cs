using System;

namespace Fleury.Agendamento.Application.UseCases.Paciente
{
    public class PacienteRequest
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}