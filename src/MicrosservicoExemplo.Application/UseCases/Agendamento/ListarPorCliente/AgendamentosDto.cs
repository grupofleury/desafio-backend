using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleury.Agendamento.Application.UseCases.Agendamento.ListarPorCliente
{
    public class AgendamentosDto
    {
        public Guid Id { get; set; }

        public Domain.Cliente.Cliente Cliente { get; set; }

        public List<Domain.Exame.ExameDto> Exames { get; set; }

        public Decimal Valor { get; set; }
    }
}
