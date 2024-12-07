using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Enum
{
    public enum NotificationType
    {
        [Display(Name = "New Message")]
        NewMessage =1,
        [Display(Name = "Mention")]
        Mention =2,
        [Display(Name = "Missed Call")]
        MissedCall=3,
        [Display(Name = "Video Call")]
        VideoCall=4,
        [Display(Name = "System")]
        System=5
    }
}
