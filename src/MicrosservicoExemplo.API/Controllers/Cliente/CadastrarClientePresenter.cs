using Fleury.Agendamento.Application.UseCases.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Cliente
{
    public class CadastrarClientePresenter : BasePresenter<ClienteResult>
    {
        protected override void OnPopular(ClienteResult resultado)
        {
            ViewModel = new CreatedResult("/", resultado);
        }
    }
}
