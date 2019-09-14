using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Fleury.Agendamento.Domain.Tests.Agendamentos
{
    public class AgendamentoTest
    {
        [Fact]
        public void Deve_gerar_agendamento_valido()
        {
            // arrange
            var cliente = GenFu.GenFu.New<Paciente.Paciente>();
            var exames = GenFu.GenFu.ListOf<Exame.Exame>(10);

            //act
            var agendamento = new Agendamento.Agendamento(cliente, exames, new DateTime(2019, 12, 12, 15, 00, 00));
            agendamento.Validate();

            // assert
            agendamento.Notifications.Should().BeEmpty();
            agendamento.DataAgendamento.Should().Be(Convert.ToDateTime("2019-12-12 15:00"));
            agendamento.Valid.Should().BeTrue();
        }


        [Fact]
        public void Deve_gerar_agendamento_data_exame_invalido()
        {
            // arrange
            var cliente = GenFu.GenFu.New<Paciente.Paciente>();
            var exames = GenFu.GenFu.ListOf<Exame.Exame>(10);

            //act
            var agendamento = new Agendamento.Agendamento(cliente, exames, new DateTime(2018, 12, 12, 15, 00, 00));
            agendamento.Validate();

            // assert

            agendamento.Valid.Should().BeFalse();
            agendamento.Notifications.Should().HaveCount(1);
            agendamento.Notifications.FirstOrDefault().Property.Should().Be(nameof(agendamento.DataAgendamento));
           
            
        }
    }
}
