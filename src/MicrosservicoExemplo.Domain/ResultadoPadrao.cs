using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fleury.Agendamento.Domain
{
    public class ResultadoPadrao
    {
        public ResultadoPadrao()
        {
            Valido = false;
            Invalido = true;
            Notificacoes = new List<Notificacao>();
        }

        [JsonProperty("valid")]
        public bool Valido { get; set; }
        [JsonProperty("invalid")]
        public bool Invalido { get; set; }
        [JsonProperty("notifications")]
        public IEnumerable<Notificacao> Notificacoes { get; set; }
       

        public bool EstaValido()
        {
            return Valido;
        }
    }
}