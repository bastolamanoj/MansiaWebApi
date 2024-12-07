using DataProvider.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Chat
{
    public class RoomUser
    {
        public string UserId { get; set; }       // Foreign Key to User
        public User User { get; set; }
        public string ChatRoomId { get; set; }   // Foreign Key to ChatRoom
        public ChatRoom ChatRoom { get; set; }
    }
}
