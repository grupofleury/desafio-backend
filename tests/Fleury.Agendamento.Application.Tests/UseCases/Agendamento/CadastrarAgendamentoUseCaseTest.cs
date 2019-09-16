using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorPaciente;
using Fleury.Agendamento.Application.UseCases.Paciente;
using Fleury.Agendamento.Domain.Agendamento.Repositorio;
using Fleury.Agendamento.Domain.Exame;
using Fleury.Agendamento.Domain.Exame.Externo;
using Fleury.Agendamento.Domain.Paciente.Repositorio;
using FluentAssertions;
using Moq;
using Xunit;

namespace Fleury.Agendamento.Application.Tests.UseCases.Agendamento
{
    public class CadastrarAgendamentoUseCaseTest
    {
        private readonly Mock<IAgendamentoRepositorio> _mockAagendamentoRepositorio =
            new Mock<IAgendamentoRepositorio>();

        private readonly Mock<IPacienteRepositorio> _mockPacienteRepositorio = new Mock<IPacienteRepositorio>();
        private readonly Mock<IExameServicoExterno> _mockExameServicoExterno = new Mock<IExameServicoExterno>();

        private CadastrarAgendamentoUseCase CadastrarAgendamentoUseCase()
        {
            return new CadastrarAgendamentoUseCase(_mockAagendamentoRepositorio.Object, _mockPacienteRepositorio.Object,
                _mockExameServicoExterno.Object);
        }

        [Fact]
        public void Deve_cadastrar_agendamento()
        {
            var agendamentoUseCase = CadastrarAgendamentoUseCase();

            var exames = GenFu.GenFu.ListOf<Exame>(25);

            var paciente = GenFu.GenFu.New<Domain.Paciente.Paciente>();

            var agendamento = new Domain.Agendamento.Agendamento
            {
                DataAgendamento = Convert.ToDateTime("2019-09-18 10:30"),
                Paciente = paciente,
                Exames = exames,
            };

            var request = new AgendamentoRequest
            {
                DataAgendamento = Convert.ToDateTime("2019-09-18 10:30"),
                Exames = exames,
                Paciente = paciente
            };

            _mockPacienteRepositorio.Setup(a => a.Salvar(It.IsAny<Domain.Paciente.Paciente>())).Returns(paciente);
            _mockPacienteRepositorio.Setup(a => a.Obter(It.IsAny<string>())).Returns(paciente);
            _mockExameServicoExterno.Setup(e => e.ObterExamesPorId(It.IsAny<List<int>>())).Returns(exames);
            _mockAagendamentoRepositorio.Setup(a => a.SalvarAgendamento(It.IsAny<Domain.Agendamento.Agendamento>())).Returns(agendamento);
               

            var resultado = agendamentoUseCase.Cadastrar(request);

            resultado.Valid.Should().BeTrue();
            resultado.Invalid.Should().BeFalse();
            resultado.Notifications.Should().BeNullOrEmpty();
            resultado.Should().BeOfType<AgendamentoResult>();
        }

        [Fact]
        public void Deve_aprensentar_erro_se_usuario_nao_estiver_cadastrado()
        {
            var agendamentoUseCase = CadastrarAgendamentoUseCase();

            var exames = GenFu.GenFu.ListOf<Exame>(25);

            var paciente = GenFu.GenFu.New<Domain.Paciente.Paciente>();

            var agendamento = new Domain.Agendamento.Agendamento
            {
                DataAgendamento = Convert.ToDateTime("2019-09-18 10:30"),
                Paciente = paciente,
                Exames = exames,
            };

            var request = new AgendamentoRequest
            {
                DataAgendamento = Convert.ToDateTime("2019-09-18 10:30"),
                Exames = exames,
                Paciente = paciente
            };

            _mockPacienteRepositorio.Setup(a => a.Salvar(It.IsAny<Domain.Paciente.Paciente>())).Returns(paciente);
            _mockPacienteRepositorio.Setup(a => a.Obter(It.IsAny<string>()));
            _mockExameServicoExterno.Setup(e => e.ObterExamesPorId(It.IsAny<List<int>>())).Returns(exames);
            _mockAagendamentoRepositorio.Setup(a => a.SalvarAgendamento(It.IsAny<Domain.Agendamento.Agendamento>())).Returns(agendamento);


            var resultado = agendamentoUseCase.Cadastrar(request);

            resultado.Should().BeOfType<AgendamentoResult>();
            resultado.Valid.Should().BeFalse();
            resultado.Invalid.Should().BeTrue();
            resultado.Notifications.Should().NotBeNullOrEmpty();
            resultado.Notifications.Should().HaveCount(1);
            resultado.Notifications.FirstOrDefault().Property.Should().Be("Cpf");
            resultado.Notifications.FirstOrDefault().Message.Should().Be("Paciente não cadastrado");
        }

        [Fact]
        public void Deve_apresentar_erro_ao_nao_informar_exames()
        {
            var agendamentoUseCase = CadastrarAgendamentoUseCase();
           

            var paciente = GenFu.GenFu.New<Domain.Paciente.Paciente>();
            var agendamento = new Domain.Agendamento.Agendamento
            {
                DataAgendamento = Convert.ToDateTime("2019-09-18 10:30"),
                Paciente = paciente,
                
            };

            var request = new AgendamentoRequest
            {
                DataAgendamento = Convert.ToDateTime("2019-09-18 10:30"),
                Paciente = paciente
            };

            _mockPacienteRepositorio.Setup(a => a.Salvar(It.IsAny<Domain.Paciente.Paciente>())).Returns(paciente);
            _mockPacienteRepositorio.Setup(a => a.Obter(It.IsAny<string>())).Returns(paciente);
            _mockAagendamentoRepositorio.Setup(a => a.SalvarAgendamento(It.IsAny<Domain.Agendamento.Agendamento>())).Returns(agendamento);


            var resultado = agendamentoUseCase.Cadastrar(request);

            resultado.Should().BeOfType<AgendamentoResult>();
            resultado.Valid.Should().BeFalse();
            resultado.Invalid.Should().BeTrue();
            resultado.Notifications.Should().NotBeNullOrEmpty();
            resultado.Notifications.Should().HaveCount(1);
            resultado.Notifications.FirstOrDefault().Property.Should().Be("Exames");
            resultado.Notifications.FirstOrDefault().Message.Should().Be("Para realizar um agendamento é obrigatório informar ao menos um exame");
            


        }
    }
}
