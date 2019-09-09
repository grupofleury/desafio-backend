using Exam.ExternalServices.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exam.ExternalServices.Services
{
    public class BaseService: IDisposable
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseService(IHttpContextAccessor httpContextAccessor, string baseUrl)
        {
            _httpContextAccessor = httpContextAccessor;
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(90)
            };
        }

        protected virtual async Task<TOutSuccess> ReturnSuccessResponseAsync<TOutSuccess>(HttpResponseMessage response)
        {
            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TOutSuccess>(result);
        }

        protected (TOutSuccess ResponseObject, HttpResponseMessage httpResponse)
            SendJsonRequest<TIn, TOutSuccess>(
            MethodHttp method,
            string endpoint,
            TIn model,
            EContentType contentType = EContentType.Json,
            bool authorize = true)
        {

            if (authorize) SetAuthorization();

            HttpResponseMessage response;

            switch (method)
            {
                case MethodHttp.GET:
                    response =  client.GetAsync(endpoint).Result;
                    break;
                case MethodHttp.POST:
                    if (contentType == EContentType.Json)
                        response =  client.PostAsync(endpoint, new StringContent(model.Serialize(), Encoding.UTF8, "application/json")).Result;
                    else if (contentType == EContentType.UrlEncoded)
                        response =  client.PostAsync(endpoint,
                            new StringContent(model.GetQueryString(), Encoding.UTF8, "application/x-www-form-urlencoded")).Result;
                    else
                        throw new NotImplementedException();
                    break;
                case MethodHttp.PUT:
                    response =  client.PutAsync(endpoint, new StringContent(model.Serialize(), Encoding.UTF8, "application/json")).Result;
                    break;
                case MethodHttp.DELETE:
                    response =  client.DeleteAsync(endpoint).Result;
                    break;
                default:
                    throw new InvalidOperationException("invalid http method or not registered");
            }

            if (!response.IsSuccessStatusCode)
            {

                return (default(TOutSuccess), response);
            }

            if (typeof(TOutSuccess) == typeof(Stream))
                return (default(TOutSuccess), response);

            var successResponse =  ReturnSuccessResponseAsync<TOutSuccess>(response).Result;


            return (successResponse, response);
        }

        protected async Task<(TOutSuccess ResponseObject, HttpResponseMessage httpResponse)>
           SendJsonRequestMEAsync<TIn, TOutSuccess>(
           MethodHttp method,
           string endpoint,
           TIn model,
           string token,
           EContentType contentType = EContentType.Json,
           bool authorize = true)
        {

            if (authorize) SetAuthorizationME(token);

            HttpResponseMessage response;

            switch (method)
            {
                case MethodHttp.GET:
                    response = await client.GetAsync(endpoint);
                    break;
                case MethodHttp.POST:
                    if (contentType == EContentType.Json)
                        response = await client.PostAsync(endpoint, new StringContent(model.Serialize(), Encoding.UTF8, "application/json"));
                    else if (contentType == EContentType.UrlEncoded)
                        response = await client.PostAsync(endpoint,
                            new StringContent(model.GetQueryString(), Encoding.UTF8, "application/x-www-form-urlencoded"));
                    else
                        throw new NotImplementedException();
                    break;
                case MethodHttp.PUT:
                    response = await client.PutAsync(endpoint, new StringContent(model.Serialize(), Encoding.UTF8, "application/json"));
                    break;
                case MethodHttp.DELETE:
                    response = await client.DeleteAsync(endpoint);
                    break;
                default:
                    throw new InvalidOperationException("invalid http method or not registered");
            }

            if (!response.IsSuccessStatusCode)
            {

                return (default(TOutSuccess), response);
            }

            if (typeof(TOutSuccess) == typeof(Stream))
                return (default(TOutSuccess), response);

            var successResponse = await ReturnSuccessResponseAsync<TOutSuccess>(response);


            return (successResponse, response);
        }

        protected virtual void SetAuthorization()
        {
            try
            {
                string bearerToken = GetToken();
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                    client.DefaultRequestHeaders.Remove("Authorization");

                client.DefaultRequestHeaders.Add("Authorization", bearerToken);
            }
            catch (Exception)
            {
            }
        }

        protected virtual void SetAuthorizationME(string token)
        {
            try
            {
                string bearerToken = token;
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                    client.DefaultRequestHeaders.Remove("Authorization");

                client.DefaultRequestHeaders.Add("Authorization", bearerToken);
            }
            catch (Exception)
            {
            }
        }

        protected void SetHeader(string name, string value)
        {
            this.client.DefaultRequestHeaders.Add(name, value);
        }

        protected virtual string GetToken()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        }

        public void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
