using System;
using System.Collections.Generic;
using System.Text;

namespace Fleury.Agendamento.Domain.Cliente.Repositorio
{
    public interface IClienteRepositorio
    {
        Cliente Obter(string cpf);
        void Salvar(Cliente cliente);
        List<Cliente> ObterClientes();

        void Atualizar(Cliente cliente);
    }
}
