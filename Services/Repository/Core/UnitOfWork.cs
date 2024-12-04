using DataProvider.DatabaseContext;
using DataProvider.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext dbContext;
        public UnitOfWork(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public void Commit()
        {
            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception  ex)
                {
                    dbContextTransaction.Rollback();
                }
                finally
                {
                    Dispose();
                }
            }
        }

        public async Task<bool> CommitAsync()
        {
            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                  await  dbContext.SaveChangesAsync();
                         dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.RollbackAsync();
                    throw ex;
                }
                finally
                {
                    Dispose();
                }
            }
        }

        public Task<bool> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
