using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.ListarPorCliente
{
    public interface IListarAgendamentoPorClienteUseCase
    {
        ListarPorClienteResult Obter(string cpf);
    }
}
