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
    public class ScheduleController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IOptions<AppSettings> _settings;

        private readonly IScheduleRepository _scheduleRepository;

        private readonly IClientRepository _clientRepository;

        private readonly IMapper _mapper;


        public ScheduleController(IDomainNotificationHandler domainNotificationHandler,
               IOptions<AppSettings> settings,
               IScheduleRepository scheduleRepository,
               IClientRepository clientRepository,
               IMapper mapper,
               IUnitOfWork unitOfWork) : base(domainNotificationHandler)
        {
            _settings = settings;
            _scheduleRepository = scheduleRepository;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostScheme(ScheduleModel scheduleModel)
        {
            try
            {
                Schedule schedule = _mapper.Map<Schedule>(scheduleModel);

                Client client = _clientRepository.GetById(schedule.ClientId);

                if (_scheduleRepository.GetByIdExamDate(schedule.IdExam, schedule.DateSchedule) == null)
                {
                    schedule.Client = client;

                    _scheduleRepository.Add(schedule);

                    _unitOfWork.SaveChanges();

                    return Ok(new { id = schedule.Id });
                }

                return ErrorResponse(new ErrorResponseModel(ScheduleErrorConstants.SchedulerExistsWithIdDate));
            }
            catch (Exception e)
            {
                return ErrorResponse(new ErrorResponseModel(e.InnerException.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutScheme(ScheduleModelPut scheduleModel)
        {
            try
            {
                Schedule schedule = _scheduleRepository.GetById(scheduleModel.IdSchedule);

                if (schedule == null)
                    return ErrorResponse(new ErrorResponseModel(ScheduleErrorConstants.SchedulerNotExist));

                schedule.DateSchedule = new DateTime(schedule.DateSchedule.Year,
                schedule.DateSchedule.Month,
                scheduleModel.DateSchedule.Day,
                scheduleModel.DateSchedule.Hour,
                schedule.DateSchedule.Minute,
                schedule.DateSchedule.Second);

                _scheduleRepository.Update(schedule);

                _unitOfWork.SaveChanges();

                return Ok(schedule);
            }
            catch (Exception e)
            {
                return ErrorResponse(new ErrorResponseModel(e.InnerException.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedule()
        {
            try
            {

                var schedule = _scheduleRepository.GetAll();

                if (schedule.Count() == 0)
                {
                    return ErrorResponse(new ErrorResponseModel(ScheduleErrorConstants.SchedulerNotExist));
                }

                return Ok(schedule);

            }
            catch (Exception e)
            {
                return ErrorResponse(new ErrorResponseModel(e.InnerException.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheme(int id)
        {
            try
            {
                Schedule schedule = _scheduleRepository.GetById(id);

                if (schedule == null)
                    return ErrorResponse(new ErrorResponseModel(ScheduleErrorConstants.SchedulerNotExist));

                _scheduleRepository.Remove(schedule);

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