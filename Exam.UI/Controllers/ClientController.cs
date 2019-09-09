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
        public async Task<IActionResult> PostClient(ClientModelCreate clientModel)
        {
            try
            {
                var client = new Client(clientModel.Name,clientModel.Cpf,clientModel.DateBirth);

                _clientRepository.Add(client);

                _unitOfWork.SaveChanges();

                return Ok(new { id = client.Id });

            }
            catch (Exception e)
            {
                return ErrorResponse(new ErrorResponseModel(e.InnerException.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutClient(ClientModel clientModel)
        {
            try
            {

                var client = _clientRepository.GetById(clientModel.Id);

                if (client == null)
                {
                    return ErrorResponse(new ErrorResponseModel(ClientErrorConstants.ClientNotFound));
                }

                client.Cpf = clientModel.Cpf;
                client.DateBirth = clientModel.DateBirth;
                client.Name = clientModel.Name;

                _clientRepository.Update(client);

                _unitOfWork.SaveChanges();

                return Ok(client);

            }
            catch (Exception e)
            {
                return ErrorResponse(new ErrorResponseModel(e.InnerException.Message));
            }
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> GetClientCpf(string cpf)
        {
            try
            {

                var client = _clientRepository.GetClientByCpf(cpf);

                if (client == null)
                {
                    return ErrorResponse(new ErrorResponseModel(ClientErrorConstants.ClientNotFound));
                }

                return Ok(client);

            }
            catch (Exception e)
            {
                return ErrorResponse(new ErrorResponseModel(e.InnerException.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetClient()
        {
            try
            {

                var client = _clientRepository.GetAll();

                if (client.Count() == 0)
                {
                    return ErrorResponse(new ErrorResponseModel(ClientErrorConstants.ClientNotExists));
                }

                return Ok(client);

            }
            catch (Exception e)
            {
                return ErrorResponse(new ErrorResponseModel(e.InnerException.Message));
            }
        }

        [HttpGet("schedule/{cpf}")]
        public async Task<IActionResult> GetClientSchedule(string cpf)
        {
            try
            {

                var client = _clientRepository.GetAllScheduleByCpf(cpf);

                if (client == null)
                {
                    return ErrorResponse(new ErrorResponseModel(ClientErrorConstants.ClientNotExists));
                }

                var clientModel = new ClientModelGet(client.Name, client.Cpf, client.DateBirth, client.Id);
                clientModel.ScheduleModels = new List<ScheduleModel>();

                client.Schedules.ForEach(x =>
                {
                    var scheduleModel = new ScheduleModel(x.IdExam, x.Name, x.Value, x.DateSchedule, x.ClientId);
                    clientModel.TotalValue = clientModel.TotalValue + x.Value;
                    clientModel.ScheduleModels.Add(scheduleModel);
                });
                return Ok(clientModel);

            }
            catch (Exception e)
            {
                return ErrorResponse(new ErrorResponseModel(e.InnerException.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var client = _clientRepository.GetAllScheduleById(id);

                if (client == null)
                    return ErrorResponse(new ErrorResponseModel(ClientErrorConstants.ClientNotExists));

                _clientRepository.Remove(client);

                _unitOfWork.SaveChanges();

                return Ok(new { id = id });
            }
            catch (Exception e)
            {
                return ErrorResponse(new ErrorResponseModel(e.InnerException.Message));
            }
        }
    }
}
