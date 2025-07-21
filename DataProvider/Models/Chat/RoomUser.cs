using DataProvider.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Chat
{
    public class RoomUser
    {
        public string Id { get; set; }
        public string UserId { get; set; }       // Foreign Key to User
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public string ChatRoomId { get; set; }   // Foreign Key to ChatRoom
        [ForeignKey(nameof(ChatRoomId))]
        public ChatRoom ChatRoom { get; set; }
    }
}
