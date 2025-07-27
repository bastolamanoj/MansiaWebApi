using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Enum
{
    public enum CallType
    {
        [Display(Name = "Audio Call")]
        AudioCall =1,

        [Display(Name = "Video Call")]
        VideoCall =2
    }
}
