using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Enums.Event
{
    public enum EventEnum
    {
        [Display(Name = "Rossz termés")]
        BadHarvest = 0,

        [Display(Name = "Jó termés")]
        GoodHarvest = 1,

        [Display(Name = "Elégedett emberek")]
        SatisfiedPeople = 2,

        [Display(Name = "Elégedetlen emberek")]
        UnsatisfiedPeople = 3,

        [Display(Name = "Jackpot")]
        Jackpot = 4,

        [Display(Name = "Elégedett katonák")]
        SatisfiedUnits = 5,

        [Display(Name = "Elégedetlen katonák")]
        UnsatisfiedUnits = 6
    }
}
