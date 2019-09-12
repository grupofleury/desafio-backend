using System.Collections.Generic;
using System.Linq;
using Fleury.Agendamento.Domain.Cliente;
using Fleury.Agendamento.Domain.Cliente.Repositorio;

namespace Fleury.Agendamento.Infrastructure.Data
{
    public class InMemoryClientRepository : IClienteRepositorio
    {
        private readonly Dictionary<string, Cliente> _db = new Dictionary<string, Cliente>();

        public Cliente Obter(string cpf)
        {
            if (!_db.ContainsKey(cpf))
            {
                return null;
            }

            return _db[cpf];
        }

        public void Salvar(Cliente cliente)
        {
            _db[cliente.Cpf] = cliente;
        }

        public List<Cliente> ObterClientes()
        {
            return _db.Values.ToList();
        }

        public void Atualizar(Cliente cliente)
        {
            if (_db.ContainsKey(cliente.Cpf))
            {
                _db[cliente.Cpf] = cliente;
            }
        }
    }
}
