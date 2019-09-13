using System.Collections.Generic;
using System.Linq;
using Fleury.Agendamento.Domain.Exame;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.ListarPorCliente
{
    public class ListarPorClienteResult : Result
    {
        public List<Domain.Agendamento.Agendamento> Agendamentos { get; set; }

        internal void FromDomain(List<Domain.Agendamento.Agendamento> agendamentos)
        {
            Agendamentos = agendamentos.Select(x => new Domain.Agendamento.Agendamento
            {
                Cliente = x.Cliente,
                Exames = x.Exames,
                Valor = x.Exames.Sum(dto => dto.Valor)
            
            }).ToList();
        }
    }
}
