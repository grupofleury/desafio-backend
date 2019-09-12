namespace Fleury.Agendamento.Application.UseCases.Cliente
{
    public interface IClienteUseCase
    {
        ClienteResult Cadastrar(ClienteRequest request);
        ClienteResult Atualizar(ClienteRequest request);
        ClienteResult Excluir(string cpf);
        ClienteResult ObterTodos();
    }
}