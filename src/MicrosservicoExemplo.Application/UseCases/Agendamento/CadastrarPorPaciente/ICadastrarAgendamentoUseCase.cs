namespace Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorPaciente
{
    public interface ICadastrarAgendamentoUseCase
    {
        AgendamentoResult Cadastrar(AgendamentoRequest request);
    }
}
