using Exam.Domain;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces.Repositories;
using Exam.ExternalServices.Enums;
// using Exam.ExternalServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exam.ExternalServices.Exams
{
    // public class Exams: BaseService, IExamsRepository
    // {
    //     private readonly IOptions<AppSettings> _settings;


    //     public User(IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> settings) :
    //         base(httpContextAccessor, settings.Value.MeuEinsteinUrl)
    //     {
    //         _settings = settings;

    //     }

    //     public async Task<UserMeuEinstein> GetUserByDocument(string document, string token) {

    //         var result = await base.SendJsonRequestMEAsync<UserMeuEinstein, UserMeuEinstein>(MethodHttp.GET, "/api/Account/"+document+"/document", new UserMeuEinstein(),token, EContentType.Json, true);
    //         result.httpResponse.EnsureSuccessStatusCode();

    //         return result.ResponseObject;
    //     }
    // }
}