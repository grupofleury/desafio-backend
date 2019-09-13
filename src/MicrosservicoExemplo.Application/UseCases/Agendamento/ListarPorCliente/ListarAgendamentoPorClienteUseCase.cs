using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente;
using Fleury.Agendamento.Domain.Agendamento.Repositorio;
using Fleury.Agendamento.Domain.Cliente.Repositorio;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.ListarPorCliente
{
    public class ListarAgendamentoPorClienteUseCase : IListarAgendamentoPorClienteUseCase
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;

        public ListarAgendamentoPorClienteUseCase(IClienteRepositorio clienteRepositorio, IAgendamentoRepositorio agendamentoRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _agendamentoRepositorio = agendamentoRepositorio;
        }

        public ListarPorClienteResult Obter(string cpf)
        {

            var resultado = new ListarPorClienteResult();

            if (_clienteRepositorio.Obter(cpf) == null)
            {
                resultado.AddNotification(nameof(cpf), "Cliente nao cadastrado");
                resultado.Error = ErrorCode.Business;
                return resultado;
            }

            var agendamentos = _agendamentoRepositorio.ObterAgendamentosPorCliente(cpf);

            resultado.FromDomain(agendamentos.OrderBy(a => a.Cliente.Nome).ToList());

            return resultado;
        }
    }
}
