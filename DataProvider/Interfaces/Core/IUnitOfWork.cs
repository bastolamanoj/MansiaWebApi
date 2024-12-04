using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();   
        Task<bool> CommitAsync();
        Task<bool> CompleteAsync();
    }
}
