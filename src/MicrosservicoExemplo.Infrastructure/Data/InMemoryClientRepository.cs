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

        public Cliente Salvar(Cliente cliente)
        {
            _db[cliente.Cpf] = cliente;
            return cliente;
        }

        public List<Cliente> ObterClientes()
        {
            return _db.Values.ToList();
        }

        public Cliente Atualizar(Cliente cliente)
        {
            if (_db.ContainsKey(cliente.Cpf))
            {
                _db[cliente.Cpf] = cliente;
                return cliente;
            }

            return null;
        }

        public Cliente Excluir(string cpf)
        {
            Cliente cliente = null;

            if (_db.ContainsKey(cpf))
            {
                 cliente = _db[cpf];
                _db.Remove(cpf);
               
            }

            return cliente;
        }
    }
}
