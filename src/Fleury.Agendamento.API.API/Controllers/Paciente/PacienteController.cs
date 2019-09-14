using System.Collections.Generic;
using Fleury.Agendamento.Application.UseCases.Paciente;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Paciente
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteUseCase _pacienteUseCase;

        public PacienteController(IPacienteUseCase pacienteUseCase)
        {
            _pacienteUseCase = pacienteUseCase;
        }

        /// <summary>
        /// Cria um novo paciente
        /// </summary>
        /// <param name="request">Parametros para criação de um paciente</param>
        /// <response code="201">Paciente criada com sucesso</response>
        /// <response code="422">Ocorreu um erro de validação durante a criação do paciente</response>
        [HttpPost("cadastrar-paciente")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Post([FromBody]PacienteRequest request)
        {
            
            var resultado = _pacienteUseCase.Cadastrar(request);
            var presenter = new PacientePresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }


        /// <summary>
        /// Alterar paciente
        /// </summary>
        /// <param name="request">Parametros para alterar um paciente</param>
        /// <response code="200">Paciente alterado com sucesso</response>
        /// <response code="422">Ocorreu um erro de validação durante a alteração do paciente</response>
        [HttpPut("alterar-paciente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Put([FromBody]PacienteRequest request)
        {

            var resultado = _pacienteUseCase.Atualizar(request);
            var presenter = new PacientePresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }

        /// <summary>
        /// Excluir paciente
        /// </summary>
        /// <param name="request">Parametros para excluir um paciente</param>
        /// <response code="200">Paciente excluido com sucesso</response>
        /// <response code="204">Ocorreu um erro de validação durante a exclusão do paciente</response>
        [HttpDelete("excluir-paciente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 204)]
        public IActionResult Delete([FromBody]PacienteRequest request)
        {

            var resultado = _pacienteUseCase.Excluir(request.Cpf);
            var presenter = new PacientePresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }

        /// <summary>
        /// Busca de um paciente baseado no seu cpf
        /// </summary>
        /// <param name="cpf">Parametros pesquisar um paciente</param>
        /// <response code="200">Paciente retornado com sucesso</response>
        /// <response code="204">Ocorreu um erro de validação durante a busca do paciente</response>
        [HttpGet("{cpf}/obter-paciente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult ObterPorCpf(string cpf)
        {

            var resultado = _pacienteUseCase.ObterPorCpf(cpf);
            var presenter = new PacientePresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }

        /// <summary>
        /// Listagem de todos os clientes cadastrados
        /// </summary>
        /// <response code="200">Clientes retornado com sucesso</response>
        /// <response code="204">Ocorreu um erro de validação durante a busca dos clientes</response>
        [HttpGet("obter-pacientes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 422)]
        public IActionResult Obter()
        {
            var resultado = _pacienteUseCase.ObterTodos();
            var presenter = new PacienteListPresenter();
            presenter.Popular(resultado);
            return presenter.ViewModel;
        }
    }
}
