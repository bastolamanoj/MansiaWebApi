using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces.Core
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById();
        //Task<bool> Add(T entity);
        void Add(T entity);
        Task<T> AddAsync(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);

    }
}
