using DataProvider.Interfaces.Core;
using DataProvider.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces.Chat
{
    public interface IUserConnectionRepository : IBaseRepository<UserConnection>
    {
        Task<bool> SaveUserConnection();
        Task<bool> RemoveUserConnection();
    }
}
