using System.Collections.Generic;
using System.Linq;

namespace Fleury.Agendamento.Application.UseCases.Paciente.ListarPacientes
{
    public class ClienteListResult : Result
    {
        public List<Domain.Paciente.Paciente> Clientes { get; set; }

        internal void FromListDomain(List<Domain.Paciente.Paciente> clientes)
        {
            Clientes = clientes.Select(x => new Domain.Paciente.Paciente
            {
                Nome = x.Nome,
                Cpf = x.Cpf,
                DataNascimento = x.DataNascimento
            }).ToList();
        }
    }
}
