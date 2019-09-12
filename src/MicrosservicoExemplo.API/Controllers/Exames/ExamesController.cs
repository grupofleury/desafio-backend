using System.Collections.Generic;
using Fleury.Agendamento.Application.UseCases.Exames.ListarTodos;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fleury.Agendamento.API.Controllers.Exames
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ExamesController : ControllerBase
    {
        private readonly ILogger<ExamesController> _logger;
        private readonly IListarTodosUseCase _listarTodosUseCase;

        public ExamesController(ILogger<ExamesController> logger, IListarTodosUseCase listarTodosUseCase)
        {
            _logger = logger;
            _listarTodosUseCase = listarTodosUseCase;
        }


        /// <summary>
        /// Consulta exames
        /// </summary>
        /// <response code="200">Consulta realizada com sucesso</response>
        /// <response code="422">Ocorreu um erro de validação durante a consulta </response>
        [HttpGet]
        [ProducesResponseType(typeof(ListarTodosResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult ObterExames()
        {
            var resultado = _listarTodosUseCase.Executar();
            var presenter = new ListarExamesPresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }
    }
}
