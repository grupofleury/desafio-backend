using System.Collections.Generic;
using System.Linq;
using Fleury.Agendamento.Domain.Paciente;
using Fleury.Agendamento.Domain.Paciente.Repositorio;

namespace Fleury.Agendamento.Infrastructure.Data
{
    public class MemoriaPacienteRepositorio : IPacienteRepositorio
    {
        private readonly Dictionary<string, Paciente> _db = new Dictionary<string, Paciente>();

        public Paciente Obter(string cpf)
        {
            if (!_db.ContainsKey(cpf))
            {
                return null;
            }

            return _db[cpf];
        }

        public Paciente Salvar(Paciente paciente)
        {
            if (!_db.ContainsKey(paciente.Cpf))
            {
                _db[paciente.Cpf] = paciente;
                return paciente;
            }

            return null;

           
        }

        public Paciente Alterar(Paciente paciente)
        {
            _db[paciente.Cpf] = paciente;
            return paciente;
        }

        public List<Paciente> ObterClientes()
        {
            return _db.Values.ToList();
        }

      

        public Paciente Excluir(string cpf)
        {
            Paciente paciente = null;

            if (_db.ContainsKey(cpf))
            {
                 paciente = _db[cpf];
                _db.Remove(cpf);
               
            }

            return paciente;
        }
    }
}
