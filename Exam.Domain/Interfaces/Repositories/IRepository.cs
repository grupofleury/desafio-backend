using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);

        Task<TEntity> AddAsync(TEntity obj);

        Task AddRangeAsync(List<TEntity> obj);

        TEntity GetById(int id);

        Task<TEntity> GetByIdAsync(int id);

        List<TEntity> GetAll();

        void Update(TEntity obj);

        void Remove(TEntity obj);

        void Dispose();
    }
}
