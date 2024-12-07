using DataProvider.Models.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Chat
{
    public class Message
    {
        public string Id { get; set; } //this string value must be a guid string
        public string SenderId { get; set; } // User who sent the message
        public string ReceiverId { get; set; }
        public string Content { get; set; }

        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User Sender { get; set; } // Navigation property to the sender user
        public User Receiver { get; set; } // Navigation property to the receiver user
    }
}
