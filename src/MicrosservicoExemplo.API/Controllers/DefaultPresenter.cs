using Fleury.Agendamento.Application;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers
{
    public class DefaultPresenter<T> : BasePresenter<T> where T : Result
    {
        protected override void OnPopular(T resultado)
        {
            ViewModel = new OkObjectResult(resultado);
        }
    }
}
