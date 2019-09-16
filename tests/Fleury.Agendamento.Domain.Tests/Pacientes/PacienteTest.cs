using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Fleury.Agendamento.Domain.Tests.Pacientes
{
    public class PacienteTest
    {
        [Fact]
        public void Deve_gerar_paciente_valido()
        {
            // arrange
            var paciente = new Paciente.Paciente("Carlos Silva", "1254694564", new DateTime(1955, 06, 10));

            //act 
            paciente.Validate();

            // assert
            paciente.Notifications.Should().BeEmpty();
            paciente.Valid.Should().BeTrue();
        }

        [Fact]
        public void Deve_gerar_paciente_invalido()
        {
            // arrange
            var paciente = new Paciente.Paciente();
            //act 
            paciente.Validate();

            // assert
            paciente.Valid.Should().BeFalse();
            paciente.Invalid.Should().BeTrue();
            paciente.Notifications.Should().HaveCount(3);
            paciente.Notifications.FirstOrDefault().Property.Should().Be(nameof(paciente.Nome));
            paciente.Notifications.FirstOrDefault().Message.Should().Be("Informe o nome do paciente");
        }
    }
}
