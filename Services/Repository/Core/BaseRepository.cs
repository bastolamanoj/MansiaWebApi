using DataProvider.DatabaseContext;
using DataProvider.Interfaces;
using DataProvider.Interfaces.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _dbContext;
        internal DbSet<T> _dbSet;
        protected readonly ILogger _logger;

        public BaseRepository(ApplicationDbContext context, ILogger logger)
        {
            _dbContext= context;
            _logger = logger;
            this._dbSet= context.Set<T>();  
        }

        public BaseRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //public Task<bool> Add(T entity)
        //{
        //    _dbSet.Add(entity);

        //}
        public void Add(T entity)
        {
             _dbSet.Add(entity);  
            _dbContext.SaveChanges();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);  
            await _dbContext.SaveChangesAsync();
            return entity;

        }

        public Task<bool> Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return Task.FromResult(true);
            }catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this._dbSet.ToListAsync();
        }

        public async Task<T> GetById(string Id)
        {
            return await this._dbSet.FindAsync(Id);
        }

        public Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

    }

}
