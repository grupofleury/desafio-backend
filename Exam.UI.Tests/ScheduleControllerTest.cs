using AutoMapper;
using FluentAssertions;
using Exam.UI.Controllers;
using Exam.Domain.Entities;
using Exam.Domain.Events;
using Exam.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Exam.Domain;
using Microsoft.Extensions.Options;

namespace Exam.UI.Tests
{
    public class ScheduleControllerTest : ApplicationTestBase
    {
        private Mock<IUnitOfWork> _unitOfWork;

        private Mock<IOptions<AppSettings>> _settings;

        private Mock<IScheduleRepository> _scheduleRepository;

        private Mock<IClientRepository> _clientRepository;

        private Mock<IMapper> _mapper;

        private Mock<IDomainNotificationHandler> _domainNotificationHandler;


        public ScheduleControllerTest()
        {
        }

        [Fact]
        public async Task Should_be_Return_Created_Status()
        {

            var scheduleModel = Fake.Schedules.Generate(1).First();

            var schedule = Fake.Schedule.Generate(1).First();

            var client = Fake.Client.Generate(1).First();

            var instance = instanceController();

            _mapper.Setup(m => m.Map<Schedule>(scheduleModel)).Returns(schedule);

            _clientRepository.Setup(m => m.GetById(schedule.ClientId)).Returns(client);

            var result = (ObjectResult)await instance.PostScheme(scheduleModel);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_be_Return_Not_Created_Status()
        {

            var scheduleModel = Fake.Schedules.Generate(1).First();

            var schedule = Fake.Schedule.Generate(1).First();

            var client = Fake.Client.Generate(1).First();

            var instance = instanceController();

            _mapper.Setup(m => m.Map<Schedule>(scheduleModel)).Returns(schedule);

            _clientRepository.Setup(m => m.GetById(schedule.ClientId)).Returns(client);

            _scheduleRepository.Setup(m => m.GetByIdExamDate(schedule.IdExam, schedule.DateSchedule)).Returns(schedule);

            var result = (ObjectResult)await instance.PostScheme(scheduleModel);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Should_be_Return_Edited_Status()
        {

            var scheduleModel = Fake.SchedulePut.Generate(1).First();

            var schedule = Fake.Schedule.Generate(1).First();

            var instance = instanceController();

            _scheduleRepository.Setup(m => m.GetById(scheduleModel.IdSchedule)).Returns(schedule);

            var result = (ObjectResult)await instance.PutScheme(scheduleModel);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_be_Return_Not_Edited_Status()
        {

            var scheduleModel = Fake.SchedulePut.Generate(1).First();

            var instance = instanceController();

            _scheduleRepository.Setup(m => m.GetById(scheduleModel.IdSchedule));

            var result = (ObjectResult)await instance.PutScheme(scheduleModel);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Should_be_Return_Get_Status()
        {

            var schedule = Fake.Schedule.Generate(2).ToList();

            var instance = instanceController();

            _scheduleRepository.Setup(m => m.GetAll()).Returns(schedule);

            var result = (ObjectResult)await instance.GetSchedule();

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_be_Return_Not_Get_Status()
        {

            var instance = instanceController();

            _scheduleRepository.Setup(m => m.GetAll()).Returns(new List<Schedule>());

            var result = (ObjectResult)await instance.GetSchedule();

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Should_be_Return_Delete_Status()
        {

            var schedule = Fake.Schedule.Generate(1).First();

            var instance = instanceController();

            _scheduleRepository.Setup(m => m.GetById(schedule.Id)).Returns(schedule);

            var result = (ObjectResult)await instance.DeleteScheme(schedule.Id);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_be_Return_Not_Delete_Status()
        {

            var schedule = Fake.Schedule.Generate(1).First();

            var instance = instanceController();

            _scheduleRepository.Setup(m => m.GetById(schedule.Id));

            var result = (ObjectResult)await instance.DeleteScheme(schedule.Id);

            result.StatusCode.Should().Be(400);
        }

        private ScheduleController instanceController()
        {
            _clientRepository = new Mock<IClientRepository>();
            _scheduleRepository = new Mock<IScheduleRepository>();
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _settings = new Mock<IOptions<AppSettings>>();
            _domainNotificationHandler = new Mock<IDomainNotificationHandler>();

            var controller = new ScheduleController(
                _domainNotificationHandler.Object,
                _settings.Object,
                _scheduleRepository.Object,
                _clientRepository.Object,
                _mapper.Object,
                _unitOfWork.Object
                );

            return controller;
        }
    }
}
