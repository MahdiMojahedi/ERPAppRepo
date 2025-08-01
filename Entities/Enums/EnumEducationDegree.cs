using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public enum EducationDegree
    {
        [Display(Name = "دیپلم")]
        Diploma = 1,

        [Display(Name = "کاردانی")]
        Associate = 2,

        [Display(Name = "کارشناسی")]
        Bachelor = 3,

        [Display(Name = "کارشناسی ارشد")]
        Master = 4,

        [Display(Name = "دکتری")]
        PhD = 5
    }
}
