using System;
using System.Collections.Generic;
using System.Text;

namespace Fleury.Agendamento.Domain.Exame
{
    public class ExameDto
    {
        public string Name { get; set; }
        public Decimal Valor { get; set; }

        public Decimal ValorTotal { get; set; }
    }
}
