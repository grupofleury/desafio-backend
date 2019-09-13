namespace Fleury.Agendamento.Application.UseCases.Agendamento.CadastrarPorCliente
{
    public interface ICadastrarAgendamentoUseCase
    {
        AgendamentoResult Cadastrar(AgendamentoRequest request);
    }
}
