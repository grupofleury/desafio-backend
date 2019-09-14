using Fleury.Agendamento.Application.UseCases.Agendamento.ExcluirAgendamento;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Agendamento
{
    public class ExcluirAgendamentoPresenter : BasePresenter<ExcluirAgendamentoResult>
    {
        protected override void OnPopular(ExcluirAgendamentoResult resultado)
        {
            ViewModel = new CreatedResult("/", resultado);
        }
    }
}
