using System.Collections.Generic;
using Fleury.Agendamento.Domain.Exame;

namespace Fleury.Agendamento.Domain.Agendamento.Repositorio
{
    public interface IAgendamentoRepositorio
    {
        List<Agendamento> ObterAgendamentosPorCliente(string cpf);
        Agendamento SalvarAgendamento(Agendamento agendamento);
       
    }
}
