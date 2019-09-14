using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleury.Agendamento.Domain.Exame;
using Fleury.Agendamento.Domain.Exame.Externo;

namespace Fleury.Agendamento.Application.UseCases.Exames.ListarTodos
{
    public class ListarTodosUseCase : IListarTodosUseCase
    {
        private readonly IExameServicoExterno _exameServicoExterno;

        public ListarTodosUseCase(IExameServicoExterno exameServicoExterno)
        {
            _exameServicoExterno = exameServicoExterno;
        }

        public ListarTodosResult Executar()
        {
            var resultado = new ListarTodosResult();
            var exames = _exameServicoExterno.ListarTodos();
            resultado.Exames = exames;

            return resultado;
        }
    }
}
