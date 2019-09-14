using System;
using Newtonsoft.Json;

namespace Fleury.Agendamento.Domain.Exame
{
    public class Exame
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        [JsonIgnore]
        public Decimal Value { get; set; }
    }


}
