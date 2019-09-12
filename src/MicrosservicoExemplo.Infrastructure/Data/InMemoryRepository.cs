using System.Collections.Generic;
using Fleury.Agendamento.Domain.Cliente;
using Fleury.Agendamento.Domain.Cliente.Repositorio;

namespace Fleury.Agendamento.Infrastructure.Data
{
    public class InMemoryRepository : IClienteRepositorio
    {
        private readonly Dictionary<string,Cliente> _db = new Dictionary<string, Cliente> ();

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
    }
}
