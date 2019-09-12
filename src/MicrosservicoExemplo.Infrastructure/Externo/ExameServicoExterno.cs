using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using Fleury.Agendamento.Application.Settings;
using Fleury.Agendamento.Domain.Exame;
using Fleury.Agendamento.Domain.Exame.Externo;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fleury.Agendamento.Infrastructure.Externo
{
    public class ExameServicoExterno : IExameServicoExterno
    {
        private readonly IRestClient _restClient;
        private readonly Configuracoes _configuracoes;

        public ExameServicoExterno(IRestClient restClient, IOptions<Configuracoes> configuracoes)
        {
            _restClient = restClient;
            _configuracoes = configuracoes.Value;
        }


        public List<Exame> ListarTodos()
        {

            WebProxy proxy = new WebProxy("10.1.10.4:9090", true);
            proxy.Credentials = new NetworkCredential("fabio.mans", "dell2018$");
            WebRequest.DefaultWebProxy = proxy;


            var url = _configuracoes.UrlExames;
            _restClient.BaseUrl = new Uri(url);
            var request = new RestRequest(_configuracoes.UrlExames, Method.GET);
           
            var response = _restClient.Execute<Dictionary<string, List<Exame>>>(request);
            

            return response.Data["exams"];
        }
    }
}
