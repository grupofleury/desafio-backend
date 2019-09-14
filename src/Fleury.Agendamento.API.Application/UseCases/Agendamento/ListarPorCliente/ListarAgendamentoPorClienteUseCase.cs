using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleury.Agendamento.Domain.Agendamento.Repositorio;
using Fleury.Agendamento.Domain.Paciente.Repositorio;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.ListarPorCliente
{
    public class ListarAgendamentoPorClienteUseCase : IListarAgendamentoPorClienteUseCase
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IPacienteRepositorio _pacienteRepositorio;

        public ListarAgendamentoPorClienteUseCase(IPacienteRepositorio pacienteRepositorio, IAgendamentoRepositorio agendamentoRepositorio)
        {
            _pacienteRepositorio = pacienteRepositorio;
            _agendamentoRepositorio = agendamentoRepositorio;
        }

        public ListarPorClienteResult Obter(string cpf)
        {

            var resultado = new ListarPorClienteResult();

            if (_pacienteRepositorio.Obter(cpf) == null)
            {
                resultado.AddNotification(nameof(cpf), "Paciente nao cadastrado");
                resultado.Error = ErrorCode.Business;
                return resultado;
            }

            var agendamentos = _agendamentoRepositorio.ObterAgendamentosPorCliente(cpf);

            resultado.FromDomain(agendamentos.OrderBy(a => a.Paciente.Nome).ToList());

            return resultado;
        }
    }
}
