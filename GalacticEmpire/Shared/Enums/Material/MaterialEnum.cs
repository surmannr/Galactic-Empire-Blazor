using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Enums.Material
{
    public enum MaterialEnum
    {
        [Display(Name = "Bitcoin")]
        Bitcoin = 0,

        [Display(Name = "Élelem")]
        Food = 1,

        [Display(Name = "Kvarc")]
        Quartz = 2
    }
}
