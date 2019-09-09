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
    public class ClientControllerTest : ApplicationTestBase
    {
        private Mock<IUnitOfWork> _unitOfWork;

        private Mock<IOptions<AppSettings>> _settings;

        private Mock<IClientRepository> _clientRepository;

        private Mock<IMapper> _mapper;

        private Mock<IDomainNotificationHandler> _domainNotificationHandler;


        public ClientControllerTest()
        {
        }

        [Fact]
        public async Task Should_be_Return_Created_Status()
        {

            var clientModel = Fake.ClientsCreate.Generate(1).First();

            var client = Fake.Client.Generate(1).First();

            var instance = instanceController();

            _mapper.Setup(m => m.Map<Client>(clientModel)).Returns(client);

            _clientRepository.Setup(m => m.Add(client));

            var result = (ObjectResult)await instance.PostClient(clientModel);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_be_Return_Edited_Status()
        {

            var clientModel = Fake.ClientModel.Generate(1).First();

            var client = Fake.Client.Generate(1).First();

            var instance = instanceController();

            _clientRepository.Setup(m => m.GetById(clientModel.Id)).Returns(client);

            var result = (ObjectResult)await instance.PutClient(clientModel);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_be_Return_Not_Edited_Status()
        {

            var clientModel = Fake.ClientModel.Generate(1).First();

            var instance = instanceController();

            _clientRepository.Setup(m => m.GetById(clientModel.Id));

            var result = (ObjectResult)await instance.PutClient(clientModel);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Should_be_Return_GetCpf_Status()
        {

            var client = Fake.Client.Generate(1).First();

            var instance = instanceController();

            _clientRepository.Setup(m => m.GetClientByCpf(client.Cpf)).Returns(client);

            var result = (ObjectResult)await instance.GetClientCpf(client.Cpf);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_be_Return_Not_GetCpf_Status()
        {

            var client = Fake.Client.Generate(1).First();

            var instance = instanceController();

            _clientRepository.Setup(m => m.GetClientByCpf(client.Cpf));

            var result = (ObjectResult)await instance.GetClientCpf(client.Cpf);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Should_be_Return_GetAll_Status()
        {

            var client = Fake.Client.Generate(2).ToList();

            var instance = instanceController();

            _clientRepository.Setup(m => m.GetAll()).Returns(client);

            var result = (ObjectResult)await instance.GetClient();

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_be_Return_Not_GetAll_Status()
        {

            var client = Fake.Client.Generate(2).ToList();

            var instance = instanceController();

            _clientRepository.Setup(m => m.GetAll()).Returns(new List<Client>());

            var result = (ObjectResult)await instance.GetClient();

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Should_be_Return_GetClient_Status()
        {

            var client = Fake.Client.Generate(1).First();
            
            var instance = instanceController();

            var clientScheme = Fake.GenerateScheduleWithClient();
            _clientRepository.Setup(m => m.GetAllScheduleByCpf(client.Cpf)).Returns(clientScheme);

            var result = (ObjectResult)await instance.GetClientSchedule(client.Cpf);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_be_Return_Not_GetClient_Status()
        {

            var client = Fake.Client.Generate(1).First();

            var instance = instanceController();

            var clientScheme = Fake.GenerateScheduleWithClient();
            _clientRepository.Setup(m => m.GetAllScheduleByCpf(client.Cpf));

            var result = (ObjectResult)await instance.GetClientSchedule(client.Cpf);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Should_be_Return_DeleteClient_Status()
        {

            var client = Fake.Client.Generate(1).First();

            var instance = instanceController();

            _clientRepository.Setup(m => m.GetAllScheduleById(client.Id)).Returns(client);

            var result = (ObjectResult)await instance.DeleteClient(client.Id);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_be_Return_Not_DeleteClient_Status()
        {

            var client = Fake.Client.Generate(1).First();

            var instance = instanceController();

            _clientRepository.Setup(m => m.GetAllScheduleById(client.Id));

            var result = (ObjectResult)await instance.DeleteClient(client.Id);

            result.StatusCode.Should().Be(400);
        }

        private ClientController instanceController()
        {

            _clientRepository = new Mock<IClientRepository>();
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _settings = new Mock<IOptions<AppSettings>>();
            _domainNotificationHandler = new Mock<IDomainNotificationHandler>();

            var controller = new ClientController(
                _domainNotificationHandler.Object,
                _settings.Object,
                _clientRepository.Object,                
                _mapper.Object,
                _unitOfWork.Object
                );

            return controller;
        }
    }
}
