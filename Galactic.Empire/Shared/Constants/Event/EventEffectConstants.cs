using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Constants.Event
{
    public static class EventEffectConstants
    {
        // BadHarvest
        public static readonly double BadHarvest_ProductionPercentage = -0.15;

        // GoodHarvest
        public static readonly double GoodHarvest_ProductionPercentage = 0.15;

        // SatistfiedPeople
        public static readonly int SatisfiedPeople_Population = 100;

        // UnsatisfiedPeople
        public static readonly int UnsatisfiedPeople_Population = -100;

        // Jackpot
        public static readonly int Jackpot = 10000;

        // SatisfiedUnits
        public static readonly double SatisfiedUnits_AttackAndDefenseMultiplier = 0.2;

        // UnsatisfiedUnits
        public static readonly double UnsatisfiedUnits_AttackAndDefense = -0.2;
    }
}
