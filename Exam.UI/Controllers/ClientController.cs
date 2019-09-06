using AutoMapper;

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

        private readonly IUnitOfWork _unitOfWork;

        private readonly IOptions<AppSettings> _settings;

        private readonly IClientRepository _clientRepository;

        private readonly IMapper _mapper;


        public ClientController(IDomainNotificationHandler domainNotificationHandler,
               IOptions<AppSettings> settings,
               IClientRepository clientRepository,
               IMapper mapper,
               IUnitOfWork unitOfWork) : base(domainNotificationHandler)
        {
            _settings = settings;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostClient(ClientModel clientModel)
        {
            
            Client client = _mapper.Map<Client>(clientModel);

            _clientRepository.Add(client);

            _unitOfWork.SaveChanges();

            return Ok(new { id = client.Id });
        }
    }
}
