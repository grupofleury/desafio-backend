using System;
using System.Collections.Generic;
using System.Text;

namespace Fleury.Agendamento.Domain.Cliente.Repositorio
{
    public interface IClienteRepositorio
    {
        Cliente Obter(string requestCpf);
        void Salvar(Cliente conta);
    }
}
