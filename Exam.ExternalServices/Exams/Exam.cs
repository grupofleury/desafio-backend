using Exam.Domain;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces.Repositories;
using Exam.ExternalServices.Enums;
using Exam.ExternalServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exam.ExternalServices.ExamRepository
{
    public class ExamRepository: BaseService, IExamsRepository
    {
        private readonly IOptions<AppSettings> _settings;


        public ExamRepository(IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> settings) :
            base(httpContextAccessor, settings.Value.FleuryUrl)
        {
            _settings = settings;

        }

        public ExamsList GetExams() {

            var result =  base.SendJsonRequest<ExamsList, ExamsList>(MethodHttp.GET, "v2/5d681ede33000054e7e65c3f", new ExamsList(), EContentType.Json, false);
            result.httpResponse.EnsureSuccessStatusCode();

            return result.ResponseObject;
        }
    }
}