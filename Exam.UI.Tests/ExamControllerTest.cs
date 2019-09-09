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
    public class ExamControllerTest : ApplicationTestBase
    {
        private Mock<IUnitOfWork> _unitOfWork;

        private Mock<IOptions<AppSettings>> _settings;

        private Mock<IExamsRepository> _examsFleuryRepository;

        private Mock<IMapper> _mapper;

        private Mock<IDomainNotificationHandler> _domainNotificationHandler;


        public ExamControllerTest()
        {
        }

        [Fact]
        public async Task Should_be_Return_Get_Status()
        {           
            var instance = instanceController();

            var examsList = Fake.GenerateExamList();

            _examsFleuryRepository.Setup(m => m.GetExams()).Returns(examsList);

            var result = (ObjectResult)await instance.GetExams();

            result.StatusCode.Should().Be(200);
        }

        private ExamController instanceController()
        {

            _examsFleuryRepository = new Mock<IExamsRepository>();
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _settings = new Mock<IOptions<AppSettings>>();
            _domainNotificationHandler = new Mock<IDomainNotificationHandler>();

            var controller = new ExamController(
                _domainNotificationHandler.Object,
                _settings.Object,
                _examsFleuryRepository.Object,
                _mapper.Object,
                _unitOfWork.Object
                );

            return controller;
        }
    }
}
