using Fleury.Agendamento.Application.UseCases.Cliente.ListarClientes;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Cliente
{
    public class ClienteListPresenter : BasePresenter<ClienteListResult>
    {
        protected override void OnPopular(ClienteListResult resultado)
        {
            ViewModel = new OkObjectResult(resultado);
        }
    }
}
