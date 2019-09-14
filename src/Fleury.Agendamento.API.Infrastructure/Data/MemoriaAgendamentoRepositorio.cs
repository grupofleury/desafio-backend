using System;
using System.Collections.Generic;
using System.Linq;
using Fleury.Agendamento.Domain.Agendamento.Repositorio;

namespace Fleury.Agendamento.Infrastructure.Data
{
    public class MemoriaAgendamentoRepositorio : IAgendamentoRepositorio
    {
        private readonly Dictionary<Guid, Domain.Agendamento.Agendamento> _db =
            new Dictionary<Guid, Domain.Agendamento.Agendamento>();

        public List<Domain.Agendamento.Agendamento> ObterAgendamentosPorCliente(string cpf)
        {
            var agendamentos = _db.Values.Where(a => a.Paciente.Cpf == cpf);
            return agendamentos.ToList();
        }

        public Domain.Agendamento.Agendamento SalvarAgendamento(Domain.Agendamento.Agendamento agendamento)
        {
            if (ValidarSeExisteExameAgendado(agendamento)) return null;

            agendamento.Id = Guid.NewGuid();
            _db[agendamento.Id] = agendamento;
            return agendamento;
        }

        private bool ValidarSeExisteExameAgendado(Domain.Agendamento.Agendamento agendamento)
        {
            var agendamentos = _db.Values;

            var agendamentoInvalido =
                agendamentos.Where(x => x.DataAgendamento == agendamento.DataAlteracaoAgendamento);

            if (agendamentoInvalido.Any())
                return true;
            return false;
        }

        public Domain.Agendamento.Agendamento AlterarAgendamento(Domain.Agendamento.Agendamento agendamento)
        {
            if (ValidarSeExisteExameAgendado(agendamento)) return null;
            var agendamentos = _db.Values;
            var agendamentoAtual = agendamentos.SingleOrDefault(a =>
                a.Paciente.Cpf == agendamento.Paciente.Cpf && a.DataAgendamento == agendamento.DataAgendamento);
            if (agendamentoAtual == null)
                return null;

            _db.Remove(agendamentoAtual.Id);
            _db.Add(agendamentoAtual.Id,
                new Domain.Agendamento.Agendamento(agendamentoAtual.Paciente, agendamentoAtual.Exames,
                    agendamento.DataAlteracaoAgendamento));


            return _db[agendamentoAtual.Id];


        }

        public bool ExcluirAgendamento(Domain.Agendamento.Agendamento agendamento)
        {
            if (ValidarSeExisteExameAgendado(agendamento)) return false;
            var agendamentos = _db.Values;
            var agendamentoAtual = agendamentos.SingleOrDefault(a =>
                a.Paciente.Cpf == agendamento.Paciente.Cpf && a.DataAgendamento == agendamento.DataAgendamento);
            if (agendamentoAtual == null)
                return false;
            _db.Remove(agendamentoAtual.Id);
            return true;
        }
    }
}

