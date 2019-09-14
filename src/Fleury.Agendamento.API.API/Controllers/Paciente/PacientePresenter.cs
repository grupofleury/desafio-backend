using Fleury.Agendamento.Application.UseCases.Paciente;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Paciente
{
    public class PacientePresenter : BasePresenter<PacienteResult>
    {
        protected override void OnPopular(PacienteResult resultado)
        {
            ViewModel = new CreatedResult("/", resultado);
        }
    }
}
