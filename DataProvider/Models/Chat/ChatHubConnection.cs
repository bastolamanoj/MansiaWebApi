using DataProvider.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Chat
{
    public class ChatHubConnection
    {
        [Key]
        public int Id { get; set; }
        public string ConnectionId { get; set; } = null!; // The SignalR connection ID for the user
        public string UserName { get; set; } = null!;
        public string UserId { get; set; }
        public User User { get; set; }  // Navigation property to the User entity

    }
}
