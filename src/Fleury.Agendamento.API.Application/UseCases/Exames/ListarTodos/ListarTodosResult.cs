using System.Collections.Generic;
using Fleury.Agendamento.Domain.Exame;

namespace Fleury.Agendamento.Application.UseCases.Exames.ListarTodos
{
    public class ListarTodosResult : Result
    {
        public List<Exame> Exames { get; set; }
    }
}
