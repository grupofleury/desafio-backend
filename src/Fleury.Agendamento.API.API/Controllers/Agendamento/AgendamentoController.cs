using System.Collections.Generic;
using Fleury.Agendamento.Application.UseCases.Agendamento.AlterarAgendamento;
using Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorPaciente;
using Fleury.Agendamento.Application.UseCases.Agendamento.ExcluirAgendamento;
using Fleury.Agendamento.Application.UseCases.Agendamento.ListarPorCliente;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using AgendamentoRequest = Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorPaciente.AgendamentoRequest;

namespace Fleury.Agendamento.API.Controllers.Agendamento
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class AgendamentoController : ControllerBase
    {
        private readonly ICadastrarAgendamentoUseCase _cadastrarAgendamentoUseCase;
        private readonly IListarAgendamentoPorClienteUseCase _agendamentoPorClienteUseCase;
        private readonly IAlterarAgendamentoUseCase _alterarAgendamentoUseCase;
        private readonly IExcluirAgendamentoUseCase _excluirAgendamentoUseCase;

        public AgendamentoController(ICadastrarAgendamentoUseCase cadastrarAgendamentoUseCase, IListarAgendamentoPorClienteUseCase agendamentoPorClienteUseCase, IAlterarAgendamentoUseCase alterarAgendamentoUseCase, IExcluirAgendamentoUseCase excluirAgendamentoUseCase)
        {
            _cadastrarAgendamentoUseCase = cadastrarAgendamentoUseCase;
            _agendamentoPorClienteUseCase = agendamentoPorClienteUseCase;
            _alterarAgendamentoUseCase = alterarAgendamentoUseCase;
            _excluirAgendamentoUseCase = excluirAgendamentoUseCase;
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

        /// <summary>
        /// Alterar agendamento
        /// </summary>
        /// <param name="request">Parametros para alterar um agendamento</param>
        /// <response code="200">Agendamento alterado com sucesso</response>
        /// <response code="422">Ocorreu um erro de validação durante a alteração do agendamento</response>
        [HttpPut("alterar-agendamento")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Put([FromBody]AlterarAgendamentoRequest request)
        {
            var resultado = _alterarAgendamentoUseCase.Alterar(request);
            var presenter = new AlterarAgendamentoPresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }

        /// <summary>
        /// Excluir um agendamento
        /// </summary>
        /// <param name="request">Parametros para excluir um agendamento</param>
        /// <response code="200">Agendamento excluido com sucesso</response>
        /// <response code="422">Ocorreu um erro exclusao do agendamento</response>
        [HttpDelete("excluir-agendamento")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Delete([FromBody]ExcluirAgendamentoRequest request)
        {
            var resultado = _excluirAgendamentoUseCase.Excluir(request);
            var presenter = new ExcluirAgendamentoPresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }
    }
}