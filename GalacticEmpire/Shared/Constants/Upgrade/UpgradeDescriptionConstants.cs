using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Constants.Upgrade
{
    public static class UpgradeDescriptionConstants
    {
        // InterdimensionalGastrogarden
        public static readonly string InterdimensionalGastrogarden_Description = $"A bolygó további {UpgradeEffectConstants.InterdimensionalGastrogarden_FoodMultiplier*100}%-kal több ételt termel a birodalom számára.";

        // FuturisticResidentialArea
        public static readonly string FuturisticResidentialArea_Description = $"A birodalomban lévő maximális populáció emelkedik {UpgradeEffectConstants.FuturisticResidentialArea_MaxPopulationIncrease} fővel és az új lakóhelyeknek köszönhetően a jelenlegi populáció {UpgradeEffectConstants.FuturisticResidentialArea_PopulationIncreaseMultiplier*100}%-kal nő.";

        // KineticShield
        public static readonly string KineticShield_Description = $"A birodalomban lévő egységek védelme {UpgradeEffectConstants.KineticShield_DefenseMultiplier*100}%-kal nő.";

        // LaserWeapons
        public static readonly string LaserWeapons_Description = $"A birodalomban lévő egységek támadása {UpgradeEffectConstants.LaserWeapons_AttackMultiplier * 100}%-kal nő.";

        // VibraniumArmor
        public static readonly string VibraniumArmor_Description = $"A birodalomban lévő egységek támadása és védelme {UpgradeEffectConstants.VibraniumArmor_AttackAndDefenseMultiplier * 100}%-kal nő.";

        // SecretMilitaryBase
        public static readonly string SecretMilitaryBase_Description = $"A birodalom maximális egységszáma {UpgradeEffectConstants.SecretMilitaryBase_UnitCount} fővel nő.";

        // QuartzMine
        public static readonly string QuartzMine_Description = $"A bolygó további {UpgradeEffectConstants.QuartzMine_QuartzMultiplier * 100}%-kal több kvarcot termel a birodalom számára.";

        // VideocardExpansion
        public static readonly string VideocardExpansion_Description = $"A bolygó további {UpgradeEffectConstants.VideocardExpansion_BitcoinMultiplier * 100}%-kal több bitcoint termel a birodalom számára.";

    }
}
