using System;
using System.Collections.Generic;
using System.Linq;
using Fleury.Agendamento.Domain.Agendamento.Repositorio;

namespace Fleury.Agendamento.Infrastructure.Data
{
    public class MemoriaAgendamentoRepositorio : IAgendamentoRepositorio
    {
        private readonly Dictionary<Guid, Domain.Agendamento.Agendamento> _db = new Dictionary<Guid, Domain.Agendamento.Agendamento>();
        public List<Domain.Agendamento.Agendamento> ObterAgendamentosPorCliente(string cpf)
        {
            var agendamentos = _db.Values.Where(a => a.Cliente.Cpf == cpf);
            return agendamentos.ToList();
        }

        public Domain.Agendamento.Agendamento SalvarAgendamento(Domain.Agendamento.Agendamento agendamento)
        {
            agendamento.Id = Guid.NewGuid();
            _db[agendamento.Id] = agendamento;
            return agendamento;
        }
    }
}
