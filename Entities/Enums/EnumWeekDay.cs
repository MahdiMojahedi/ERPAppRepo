using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public enum EnumWeekDay
    {
        [Display(Name = "شنبه")]
        Saturday = 0,
        [Display(Name = "یکشنبه")]
        Sunday = 1,
        [Display(Name = "دوشنبه")]
        Monday = 2,
        [Display(Name = "سه شنبه")]
        Tuesday = 3,
        [Display(Name = "چهارشنبه")]
        Wednesday = 4,
        [Display(Name = "شنبهپنج")]
        Thursday = 5,
        [Display(Name = "جمعه")]
        Friday = 6
    }

}
