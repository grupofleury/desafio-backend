using RestSharp;
using System.Collections.Generic;
using Fleury.Agendamento.Application.Settings;
using Fleury.Agendamento.Domain.Exame;
using Fleury.Agendamento.Domain.Exame.Externo;
using Microsoft.Extensions.Options;

namespace Fleury.Agendamento.Infrastructure.Externo
{
    public class ExameServicoExterno : IExameServicoExterno
    {
        private readonly Configuracoes _configuracoes;

        public ExameServicoExterno(IOptions<Configuracoes> configuracoes)
        {
            _configuracoes = configuracoes.Value;
        }


        public List<Exame> ListarTodos()
        {
            var url = _configuracoes.UrlExames;
            var restClient = new RestClient(url);
            var request = new RestRequest("", Method.GET);           
            var response = restClient.Execute<Dictionary<string, List<Exame>>>(request);
            return response.Data["exams"];
        }
    }
}
