using System.Collections.Generic;
using Fleury.Agendamento.API.Controllers.Cliente;
using Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente;
using Fleury.Agendamento.Application.UseCases.Agendamento.ListarPorCliente;
using Fleury.Agendamento.Application.UseCases.Cliente;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Agendamento
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class AgendamentoController : ControllerBase
    {
        private readonly ICadastrarAgendamentoUseCase _cadastrarAgendamentoUseCase;
        private readonly IListarAgendamentoPorClienteUseCase _agendamentoPorClienteUseCase;

        public AgendamentoController(ICadastrarAgendamentoUseCase cadastrarAgendamentoUseCase, IListarAgendamentoPorClienteUseCase agendamentoPorClienteUseCase)
        {
            _cadastrarAgendamentoUseCase = cadastrarAgendamentoUseCase;
            _agendamentoPorClienteUseCase = agendamentoPorClienteUseCase;
        }

        /// <summary>
        /// Cria um novo agendamento
        /// </summary>
        /// <param name="request">Parametros para de um agendamento</param>
        /// <response code="201">Agendamento criado com sucesso</response>
        /// <response code="422">Ocorreu um erro de validação durante a criação do agendamento</response>
        [HttpPost("cadastrar-agendamento")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Post([FromBody]AgendamentoRequest request)
        {
            var resultado = _cadastrarAgendamentoUseCase.Cadastrar(request);
            var presenter = new AgendamentoPresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }


        /// <summary>
        /// Cria um novo agendamento
        /// </summary>
        /// <param name="request">Parametros para de um agendamento</param>
        /// <response code="201">Agendamento criado com sucesso</response>
        /// <response code="422">Ocorreu um erro de validação durante a criação do agendamento</response>
        [HttpGet("obter-agendamento/{cpf}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Get(string cpf)
        {
            var resultado = _agendamentoPorClienteUseCase.Obter(cpf);
            var presenter = new DefaultPresenter<ListarPorClienteResult>();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }
    }
}