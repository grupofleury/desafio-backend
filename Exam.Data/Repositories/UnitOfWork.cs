using Exam.Domain.Interfaces.Repositories;
using Exam.Data.ContextConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
namespace Exam.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly DbContext _dataContext;

        public UnitOfWork(DbContext dataContext) => _dataContext = dataContext;

        public async Task<bool> SaveChangesAsync()
        {
            bool returnValue = true;

            using (var dbContextTransaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    await _dataContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ex.InnerException?.Message);
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }

            return returnValue;
        }


        public bool SaveChanges()
        {
            bool returnValue = true;

            using (var dbContextTransaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    _dataContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ex.InnerException?.Message);
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }

            return returnValue;
        }

        public void Dispose()
        {

            Dispose(true);

            _dataContext.Dispose();

            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                _dataContext.Dispose();

            disposed = true;

        }

    }
}