using Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorPaciente;
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
