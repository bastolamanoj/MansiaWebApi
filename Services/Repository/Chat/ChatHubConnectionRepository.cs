using DataProvider.DatabaseContext;
using DataProvider.Interfaces.Chat;
using DataProvider.Models.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Repository.Core;
using Services.Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.Chat
{
    public class ChatHubConnectionRepository : BaseRepository<ChatHubConnection>, IChatHubConnectionRepository
    {
        protected readonly ILogger<ChatHubConnectionRepository> _logger;
        public ChatHubConnectionRepository(ApplicationDbContext dbContext, ILogger<ChatHubConnectionRepository> logger): base(dbContext, logger)
        {
            _logger = logger;
        }

        public async Task<bool> RemoveUserConnection(string ConnectionId)
        {
            var chatHubconn = await this.GetById(ConnectionId);
            if (ConnectionId is null)
                return false;
            await this.Delete(chatHubconn);
            return true;
        }

        public async Task<bool> SaveUserConnection(ChatHubConnection chc)
        {
            if(chc is not null)
            {
                await this.AddAsync(chc);
            }
            return true;
        }
    }
}
