using DataProvider.Enum;
using DataProvider.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Chat
{
    public class UserProfile
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserOnlineStatusEnum Status { get; set; } = UserOnlineStatusEnum.Offline;
        public DateTime LastSeen { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
