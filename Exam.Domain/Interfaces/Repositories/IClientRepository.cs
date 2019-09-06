using Exam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Interfaces.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        List<Client> GetClientByCpf(string cpf);
    }
}
