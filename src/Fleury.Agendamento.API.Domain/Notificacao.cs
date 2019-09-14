using Newtonsoft.Json;

namespace Fleury.Agendamento.Domain
{
    public class Notificacao
    {
        public int CodigoErro { get; set; }
        [JsonProperty("message")]
        public string Mensagem { get; set; }
        [JsonProperty("property")]
        public string Propriedade { get; set; }
    }
}