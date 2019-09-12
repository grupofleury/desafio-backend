namespace Fleury.Agendamento.Application.UseCases.Cliente.CadastrarCliente
{
    public interface ICadastrarClienteUseCase
    {
        CadastrarClienteResult Executar(CadastrarClienteRequest request);
    }
}