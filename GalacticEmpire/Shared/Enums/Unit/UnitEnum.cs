using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Enums.Unit
{
    public enum UnitEnum
    {
        [Display(Name = "Napvitorlás")]
        SolarSail = 0,

        [Display(Name = "Űrcirkáló")]
        SpaceCruiser = 1,

        [Display(Name = "Vasember")]
        IronMan = 2,

        [Display(Name = "Ezeréves sólyom")]
        MilleniumFalcon = 3,

        [Display(Name = "Felderítő drón")]
        ScoutDrone = 4
    }
}
