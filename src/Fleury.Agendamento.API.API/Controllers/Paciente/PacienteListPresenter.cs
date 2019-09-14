using Fleury.Agendamento.Application.UseCases.Paciente.ListarPacientes;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Paciente
{
    public class PacienteListPresenter : BasePresenter<ClienteListResult>
    {
        protected override void OnPopular(ClienteListResult resultado)
        {
            ViewModel = new OkObjectResult(resultado);
        }
    }
}
