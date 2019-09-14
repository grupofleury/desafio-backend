using Microsoft.AspNetCore.Http;
using RestSharp;

namespace Fleury.Agendamento.Infrastructure.Http
{
    public class FleuryRestClient : RestClient
    {
        public readonly IHttpContextAccessor _httpContextAccessor;

        public FleuryRestClient(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var context = _httpContextAccessor.HttpContext;

            foreach (var headerKey in context.Request.Headers.Keys)
            {
                this.AddDefaultHeader(headerKey, context.Request.Headers[headerKey].ToString());
            }
        }
    }
}
