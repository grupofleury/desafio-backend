using System;
using System.Collections.Generic;
using System.Text;

namespace Fleury.Agendamento.Domain.Exame.Externo
{
    public interface IExameServicoExterno
    {
        List<Exame> ListarTodos();
    }
}
