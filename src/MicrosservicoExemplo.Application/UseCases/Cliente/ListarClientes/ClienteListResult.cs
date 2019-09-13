using System.Collections.Generic;
using System.Linq;

namespace Fleury.Agendamento.Application.UseCases.Cliente.ListarClientes
{
    public class ClienteListResult : Result
    {
        public List<Domain.Cliente.Cliente> Clientes { get; set; }

        internal void FromListDomain(List<Domain.Cliente.Cliente> clientes)
        {
            Clientes = clientes.Select(x => new Domain.Cliente.Cliente
            {
                Nome = x.Nome,
                Cpf = x.Cpf,
                DataNascimento = x.DataNascimento
            }).ToList();
        }
    }
}
