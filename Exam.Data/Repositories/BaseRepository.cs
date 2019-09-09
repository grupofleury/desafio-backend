using Exam.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _db;
        protected DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context)
        {
            _db = context;
            _dbSet = _db.Set<TEntity>();
        }

        public void Add(TEntity obj) => _db.Set<TEntity>().Add(obj);

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            await _db.Set<TEntity>().AddAsync(obj);
            return obj;
        }

        public async Task AddRangeAsync(List<TEntity> obj) => await _db.Set<TEntity>().AddRangeAsync(obj);

        public virtual TEntity GetById(int id) => _db.Set<TEntity>().Find(id);

        public Task<TEntity> GetByIdAsync(int id) => _db.Set<TEntity>().FindAsync(id);

        public virtual List<TEntity> GetAll() => _db.Set<TEntity>().ToList();

        public void Update(TEntity obj) => _db.Entry(obj).State = EntityState.Modified;

        public void Remove(TEntity obj) => _db.Set<TEntity>().Remove(obj);

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(true);
        }
    }
}
