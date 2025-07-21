using DataProvider.Enum;
using DataProvider.Models.Chat;
using DataProvider.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
    public class Notification
    {
        public string Id { get; set; }
        public string Content { get; set; } //You missed a call!
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public string NotificationType { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public string MessageId { get; set; }
        [ForeignKey(nameof(MessageId))]
        public Message Message { get; set; }
        public string CallId { get; set; }
        public CallStatusEnum CallStatus { get; set; }      

    }
}
