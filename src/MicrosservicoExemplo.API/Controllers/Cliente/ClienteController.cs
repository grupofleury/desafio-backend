using System.Collections.Generic;
using Fleury.Agendamento.Application.UseCases.Cliente;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Cliente
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteUseCase _clienteUseCase;

        public ClienteController(IClienteUseCase clienteUseCase)
        {
            _clienteUseCase = clienteUseCase;
        }

        /// <summary>
        /// Cria um novo cliente
        /// </summary>
        /// <param name="request">Parametros para criação de um cliente</param>
        /// <response code="201">Cliente criada com sucesso</response>
        /// <response code="422">Ocorreu um erro de validação durante a criação do cliente</response>
        [HttpPost("cadastrar-cliente")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Post([FromBody]ClienteRequest request)
        {
            
            var resultado = _clienteUseCase.Cadastrar(request);
            var presenter = new ClientePresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }


        /// <summary>
        /// Alterar cliente
        /// </summary>
        /// <param name="request">Parametros para alterar um cliente</param>
        /// <response code="200">Cliente alterado com sucesso</response>
        /// <response code="422">Ocorreu um erro de validação durante a alteração do cliente</response>
        [HttpPut("alterar-cliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Put([FromBody]ClienteRequest request)
        {

            var resultado = _clienteUseCase.Atualizar(request);
            var presenter = new ClientePresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }

        /// <summary>
        /// Excluir cliente
        /// </summary>
        /// <param name="request">Parametros para excluir um cliente</param>
        /// <response code="200">Cliente excluido com sucesso</response>
        /// <response code="204">Ocorreu um erro de validação durante a exclusão do cliente</response>
        [HttpDelete("excluir-cliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 204)]
        public IActionResult Delete([FromBody]ClienteRequest request)
        {

            var resultado = _clienteUseCase.Excluir(request.Cpf);
            var presenter = new ClientePresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }

        /// <summary>
        /// Busca de um cliente baseado no seu cpf
        /// </summary>
        /// <param name="cpf">Parametros pesquisar um cliente</param>
        /// <response code="200">Cliente retornado com sucesso</response>
        /// <response code="204">Ocorreu um erro de validação durante a busca do cliente</response>
        [HttpGet("{cpf}/obter-cliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult ObterPorCpf(string cpf)
        {

            var resultado = _clienteUseCase.ObterPorCpf(cpf);
            var presenter = new ClientePresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }

        /// <summary>
        /// Listagem de todos os clientes cadastrados
        /// </summary>
        /// <response code="200">Clientes retornado com sucesso</response>
        /// <response code="204">Ocorreu um erro de validação durante a busca dos clientes</response>
        [HttpGet("obter-clientes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Obter()
        {
            var resultado = _clienteUseCase.ObterTodos();
            var presenter = new ClienteListPresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }
    }
}
