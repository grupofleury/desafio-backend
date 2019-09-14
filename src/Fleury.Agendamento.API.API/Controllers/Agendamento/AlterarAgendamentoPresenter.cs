using Fleury.Agendamento.Application.UseCases.Agendamento.AlterarAgendamento;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Agendamento
{
    public class AlterarAgendamentoPresenter : BasePresenter<AlterarAgendamentoResult>
    {
        protected override void OnPopular(AlterarAgendamentoResult resultado)
        {
            ViewModel = new CreatedResult("/", resultado);
        }
    }
}
