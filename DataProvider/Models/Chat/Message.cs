using DataProvider.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Chat
{
    public class Message
    {
        public string Id { get; set; } //this string value must be a guid string
        public Guid SenderId { get; set; } // User who sent the message
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }

        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [ForeignKey(nameof(SenderId))]  
        public User Sender { get; set; } // Navigation property to the sender user
        [ForeignKey(nameof(ReceiverId))]
        public User Receiver { get; set; } // Navigation property to the receiver user
    }
}
