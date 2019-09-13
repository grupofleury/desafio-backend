using System;
using Fleury.Agendamento.Application.UseCases.Cliente;
using Fleury.Agendamento.Domain.Agendamento.Repositorio;
using Fleury.Agendamento.Domain.Cliente.Repositorio;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente
{
    public class CadastrarAgendamentoUseCase : ICadastrarAgendamentoUseCase
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;

        public CadastrarAgendamentoUseCase(IAgendamentoRepositorio agendamentoRepositorio, IClienteRepositorio clienteRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _clienteRepositorio = clienteRepositorio;
        }

        public AgendamentoResult Cadastrar(AgendamentoRequest request)
        {
            var resultado = new AgendamentoResult();

            if (_clienteRepositorio.Obter(request.Cliente.Cpf) == null)
            {
                resultado.AddNotification(nameof(request.Cliente.Cpf), "Cliente cadastrado");
                resultado.Error = ErrorCode.Business;
                return resultado;

            }

            var agendamento =
                new Domain.Agendamento.Agendamento(request.Cliente, request.Exames);

            if (agendamento.Invalid)
            {
                resultado.AddNotifications(agendamento.Notifications);
                return resultado;
            }

            _agendamentoRepositorio.SalvarAgendamento(agendamento);
            return AgendamentoResult.FromDomain(agendamento);
        }
    }
}
