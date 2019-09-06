using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Exam.Domain;
using Exam.Domain.Entities;
using Exam.Domain.Events;
using Exam.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Exam.UI.Models;

namespace Exam.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController
    {

        private readonly IOptions<AppSettings> _settings;

        public ClientController(IDomainNotificationHandler domainNotificationHandler,
               IOptions<AppSettings> settings) : base(domainNotificationHandler)
        {
            _settings = settings;

        }

    }
}
