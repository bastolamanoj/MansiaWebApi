using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Enum
{
    public enum CallStatusEnum
    {
        [Display(Name ="Missed Call")]
        Missed =1,
        [Display(Name = "Incoming Call")]
        Incoming =2,
        [Display(Name = "Outgoing Call")]
        Outgoing =3,
        [Display(Name = "Finished Call")]
        Completed =4
    }
}
