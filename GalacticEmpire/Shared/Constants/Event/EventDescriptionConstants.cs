using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Constants.Event
{
    public static class EventDescriptionConstants
    {
        // BadHarvest
        public static readonly string BadHarvest_Description = $"A termelés csökken {EventEffectConstants.BadHarvest_ProductionPercentage * 100}%-kal a rossz termelés miatt.";

        // GoodHarvest
        public static readonly string GoodHarvest_Description = $"A termelés nő {EventEffectConstants.GoodHarvest_ProductionPercentage * 100}%-kal a jó termelés miatt.";
        
        // SatisfiedPeople
        public static readonly string SatisfiedPeople_Description = $"A lakosság nő {EventEffectConstants.SatisfiedPeople_Population} fővel, mert az emberek jól érzik magukat a birodalomban.";

        // UnsatisfiedPeople
        public static readonly string UnsatisfiedPeople_Description = $"A lakosság csökken {EventEffectConstants.UnsatisfiedPeople_Population} fővel, mert az emberek nem érzik jól magukat a birodalomban.";

        // Jackpot
        public static readonly string Jackpot_Description = $"Találtál egy elhagyott rakományt, amivel találtál nyersanyagokat! Mindeből {EventEffectConstants.Jackpot} db jóváírva a birodalomhoz.";

        // SatisfiedUnits
        public static readonly string SatisfiedUnits_Description = $"Katonáid elégedettek, ezért {EventEffectConstants.SatisfiedUnits_AttackAndDefenseMultiplier*100}%-kal nagyobb a támadó és védekező erejük.";

        // UnsatisfiedUnits
        public static readonly string UnsatisfiedUnits_Description = $"Katonáid elégedetlenek, ezért {EventEffectConstants.SatisfiedUnits_AttackAndDefenseMultiplier * 100}%-kal kevesebb a támadó és védekező erejük.";
    }
}
