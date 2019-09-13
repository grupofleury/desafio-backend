using Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Agendamento
{
    public class AgendamentoPresenter : BasePresenter<AgendamentoResult>
    {
        protected override void OnPopular(AgendamentoResult resultado)
        {
            ViewModel = new CreatedResult("/", resultado);
        }
    }
}
