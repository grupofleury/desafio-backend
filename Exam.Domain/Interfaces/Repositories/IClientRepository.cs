using Exam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Interfaces.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Client GetClientByCpf(string cpf);

        Client GetAllScheduleByCpf(string cpf);

        Client GetAllScheduleById(int id);
    }
}
