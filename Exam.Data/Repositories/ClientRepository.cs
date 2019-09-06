using Exam.Domain.Entities;
using Exam.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exam.Data.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(DbContext context) : base(context)
        {
        }

        public List<Client> GetClientByCpf(string cpf)
        {
            var query = _dbSet.Where(x => x.Cpf == cpf).ToList();
            return query;
        }
    }
}
