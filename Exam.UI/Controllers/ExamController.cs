using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Exam.Domain;
using Exam.Domain.Consts;
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
    public class ExamController: BaseController
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IOptions<AppSettings> _settings;

        private readonly IExamsRepository _examsFleuryRepository;

        private readonly IMapper _mapper;


        public ExamController(IDomainNotificationHandler domainNotificationHandler,
               IOptions<AppSettings> settings,
               IExamsRepository examsFleuryRepository,
               IMapper mapper,
               IUnitOfWork unitOfWork) : base(domainNotificationHandler)
        {
            _settings = settings;
            _examsFleuryRepository = examsFleuryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetExams()
        {
            return Ok(_examsFleuryRepository.GetExams());
        }
    }
}