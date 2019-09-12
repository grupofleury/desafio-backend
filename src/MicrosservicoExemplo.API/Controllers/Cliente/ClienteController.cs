using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleury.Agendamento.Application.UseCases.Cliente.CadastrarCliente;
using Microsoft.AspNetCore.Mvc;

namespace Fleury.Agendamento.API.Controllers.Cliente
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ClienteController : ControllerBase
    {
        private readonly ICadastrarClienteUseCase _cadastrarClienteUseCase;
    }
}
