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

        public Client GetClientByCpf(string cpf)
        {
            return _dbSet.Where(x => x.Cpf == cpf).FirstOrDefault();
        }

        public Client GetAllScheduleByCpf(string cpf) =>
        _dbSet.Where(x => x.Cpf == cpf)
        .Include(x => x.Schedules).FirstOrDefault();

        public Client GetAllScheduleById(int id) =>
        _dbSet.Where(x => x.Id == id)
        .Include(x => x.Schedules).FirstOrDefault();

    }
}
