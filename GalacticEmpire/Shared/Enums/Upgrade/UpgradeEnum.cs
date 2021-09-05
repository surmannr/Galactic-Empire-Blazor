using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Enums.Upgrade
{
    public enum UpgradeEnum
    {
        [Display(Name = "Interdimenzionális gasztrokert")]
        InterdimensionalGastrogarden = 0,

        [Display(Name = "Futurisztikus lakónegyed")]
        FuturisticResidentialArea = 1,

        [Display(Name = "Kinetikus pajzs")]
        KineticShield = 2,

        [Display(Name = "Lézerfegyverek")]
        LaserWeapons = 3,

        [Display(Name = "Vibránium páncél")]
        VibraniumArmor = 4,

        [Display(Name = "Titkos katonai bázis")]
        SecretMilitaryBase = 5,

        [Display(Name = "Kvarcbánya")]
        QuartzMine = 6,

        [Display(Name = "Videókártya bővítés")]
        VideocardExpansion = 7
    }
}
