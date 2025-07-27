using DataProvider.DatabaseContext;
using DataProvider.Interfaces.Chat;
using DataProvider.Models.Chat;
using DataProvider.Models.Identity;
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
    public class ChatRoomRepository : BaseRepository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(ApplicationDbContext context, ILogger<ChatRoomRepository> logger) : base(context, logger)
        {

        }
    }
}
