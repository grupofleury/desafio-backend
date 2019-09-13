using System;
using System.Collections.Generic;
using System.Text;

namespace Fleury.Agendamento.Domain.Cliente.Repositorio
{
    public interface IClienteRepositorio
    {
        Cliente Obter(string cpf);
        Cliente Salvar(Cliente cliente);
        List<Cliente> ObterClientes();

        Cliente Atualizar(Cliente cliente);
        Cliente Excluir(string cpf);
    }
}
