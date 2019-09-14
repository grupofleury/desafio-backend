using System;
using System.Linq;
using Fleury.Agendamento.Application.UseCases.Paciente;
using Fleury.Agendamento.Domain.Paciente.Repositorio;
using FluentAssertions;
using Moq;
using Xunit;

namespace Fleury.Agendamento.Application.Tests.UseCases.Paciente
{
    public class CadastrarPacienteUseCaseTest
    {
        readonly Mock<IPacienteRepositorio> _mockPacienteRepositorio = new Mock<IPacienteRepositorio>();

        private PacienteUseCase PacienteUseCase()
        {
            return new PacienteUseCase(_mockPacienteRepositorio.Object);
        }

        [Fact]
        public void Deve_cadastrar_paciente()
        {
            var pacienteUseCase = PacienteUseCase();

            var request = new PacienteRequest
            {
                Cpf = "1254566456",
                DataNascimento = new DateTime(1978, 10, 01),
                Nome = "Jose"
            };

            _mockPacienteRepositorio.Setup(p => p.Salvar(It.IsAny<Domain.Paciente.Paciente>()));

            var resultado = pacienteUseCase.Cadastrar(request);

            resultado.Valid.Should().BeTrue();
            resultado.Invalid.Should().BeFalse();
            resultado.Notifications.Should().BeNullOrEmpty();
            resultado.Should().BeOfType<PacienteResult>();
        }



        [Fact]
        public void Deve_apresentar_erro_quando_request_nulo()
        {
            var pacienteUseCase = PacienteUseCase();

            _mockPacienteRepositorio.Setup(p => p.Salvar(It.IsAny<Domain.Paciente.Paciente>()));

            var resultado = pacienteUseCase.Cadastrar(null);

            resultado.Should().BeOfType<PacienteResult>();
            resultado.Valid.Should().BeFalse();
            resultado.Invalid.Should().BeTrue();
            resultado.Notifications.Should().NotBeNullOrEmpty();
            resultado.Notifications.Should().HaveCount(1);
            resultado.Notifications.FirstOrDefault().Property.Should().Be("Request");
            resultado.Notifications.FirstOrDefault().Message.Should().Be("Requisição inválida");
            
        }

        [Fact]
        public void Deve_aprensentar_erro_ao_nao_informar_data_nascimento()
        {
            var pacienteUseCase = PacienteUseCase();

            var request = new PacienteRequest
            {
                Cpf = "1254566456",
                Nome = "Jose"
            };

            _mockPacienteRepositorio.Setup(p => p.Salvar(It.IsAny<Domain.Paciente.Paciente>()));

            var resultado = pacienteUseCase.Cadastrar(request);

            resultado.Should().BeOfType<PacienteResult>();
            resultado.Valid.Should().BeFalse();
            resultado.Invalid.Should().BeTrue();
            resultado.Notifications.Should().NotBeNullOrEmpty();
            resultado.Notifications.Should().HaveCount(1);
            resultado.Notifications.FirstOrDefault().Property.Should().Be("DataNascimento");
            resultado.Notifications.FirstOrDefault().Message.Should().Be("Data de nascimento inválida");
        }

        [Fact]
        public void Deve_aprensentar_erro_ao_cadastrar_mesmo_paciente()
        {
            var pacienteUseCase = PacienteUseCase();

            var request = new PacienteRequest
            {
                Cpf = "1254566456",
                DataNascimento = new DateTime(1978, 10, 01),
                Nome = "Jose"
            };

            _mockPacienteRepositorio.Setup(p => p.Salvar(It.IsAny<Domain.Paciente.Paciente>()))
                .Returns<Domain.Paciente.Paciente>(null);

            var resultado = pacienteUseCase.Cadastrar(request);
            

            resultado.Should().BeOfType<PacienteResult>();
            resultado.Valid.Should().BeFalse();
            resultado.Invalid.Should().BeTrue();
            resultado.Notifications.Should().NotBeNullOrEmpty();
            resultado.Notifications.Should().HaveCount(1);
            resultado.Notifications.FirstOrDefault().Property.Should().Be("Paciente");
            resultado.Notifications.FirstOrDefault().Message.Should().Be("Paciente já cadastrado");
        }


    }
}
