using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Chat
{
    public class ChatRoom
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //Navigation Property
        public ICollection<RoomUser> RoomUsers { get; set; } // List of users in the room
        public ICollection<Message> Messages { get; set; }
    }
}
