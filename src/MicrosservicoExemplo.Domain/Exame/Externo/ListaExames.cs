using System;
using System.Collections.Generic;
using System.Text;

namespace Fleury.Agendamento.Domain.Exame.Externo
{
    public class ListaExames : ResultadoPadrao
    {
        public ICollection<Exame> Exames { get; set; }
    }
}
