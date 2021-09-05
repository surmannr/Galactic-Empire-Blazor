using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Enums.Planet
{
    public enum PlanetEnum
    {
        [Display(Name = "Föld C-137")]
        C137Earth = 0,

        [Display(Name = "Gingeria")]
        Gingeria = 1,

        [Display(Name = "Sidatania")]
        Sidatania = 2,

        [Display(Name = "Zuccars")]
        Zuccars = 3,

        [Display(Name = "Avypso")]
        Avypso = 4,

        [Display(Name = "Heolara")]
        Heolara = 5,

        [Display(Name = "Yoiphus")]
        Yoiphus = 6,

        [Display(Name = "Cribatune")]
        Cribatune = 7,

        [Display(Name = "Nusobos")]
        Nusobos = 8,

        [Display(Name = "Dillon")]
        Dillon = 9,

        [Display(Name = "Darvis")]
        Darvis = 10
    }
}
