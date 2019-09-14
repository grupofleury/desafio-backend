using Fleury.Agendamento.Application.UseCases.Paciente.ListarPacientes;

namespace Fleury.Agendamento.Application.UseCases.Paciente
{
    public interface IPacienteUseCase
    {
        PacienteResult Cadastrar(PacienteRequest request);
        PacienteResult Atualizar(PacienteRequest request);
        PacienteResult Excluir(string cpf);
        ClienteListResult ObterTodos();
        PacienteResult ObterPorCpf(string cpf);
    }
}