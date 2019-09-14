using System.Threading.Tasks;

namespace Fleury.Agendamento.Application.UseCases.Exames.ListarTodos
{
    public interface IListarTodosUseCase
    {
        ListarTodosResult Executar();
    }
}
