using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var presenter = new CadastrarClientePresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }


        /// <summary>
        /// Alterar cliente
        /// </summary>
        /// <param name="request">Parametros para alterar um cliente</param>
        /// <response code="201">Cliente alterado com sucesso</response>
        /// <response code="422">Ocorreu um erro de validação durante a alteração do cliente</response>
        [HttpPut("alterar-cliente")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Put([FromBody]ClienteRequest request)
        {

            var resultado = _clienteUseCase.Atualizar(request);
            var presenter = new CadastrarClientePresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }
    }
}
