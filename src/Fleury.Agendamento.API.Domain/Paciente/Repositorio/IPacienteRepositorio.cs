using System.Collections.Generic;

namespace Fleury.Agendamento.Domain.Paciente.Repositorio
{
    public interface IPacienteRepositorio
    {
        Paciente Obter(string cpf);
        Paciente Salvar(Paciente paciente);
        Paciente Alterar(Paciente paciente);
        List<Paciente> ObterClientes();
        Paciente Excluir(string cpf);
    }
}
