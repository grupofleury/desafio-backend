using Fleury.Agendamento.Application.UseCases.Exames.ListarTodos;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Exames
{
    public class ListarExamesPresenter : BasePresenter<ListarTodosResult>
    {
        protected override void OnPopular(ListarTodosResult resultado)
        {
            ViewModel = new CreatedResult("/", resultado);
        }
    }
}
