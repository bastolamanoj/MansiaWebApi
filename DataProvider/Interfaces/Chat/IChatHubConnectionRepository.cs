using DataProvider.Interfaces.Core;
using DataProvider.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces.Chat
{
    public interface IChatHubConnectionRepository : IBaseRepository<ChatHubConnection>
    {
        Task<bool> SaveUserConnection(ChatHubConnection chc);
        Task<bool> RemoveUserConnection(string ConnectionId);

    }
}
