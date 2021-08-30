using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.UpgradeModel.Base;
using GalacticEmpire.Shared.Constants.Time;
using GalacticEmpire.Shared.Constants.Upgrade;
using GalacticEmpire.Shared.Enums.Upgrade;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.UpgradeModel.Types
{
    public class FuturisticResidentialAreaUpgrade : Upgrade
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            empire.Population += (int)(empire.Population * UpgradeEffectConstants.FuturisticResidentialArea_PopulationIncreaseMultiplier);
            empire.MaxNumberOfPopulation += UpgradeEffectConstants.FuturisticResidentialArea_MaxPopulationIncrease;

            if(empire.Population > empire.MaxNumberOfPopulation)
            {
                empire.Population = empire.MaxNumberOfPopulation;
            }
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            empire.Population -= (int)(empire.Population * UpgradeEffectConstants.FuturisticResidentialArea_PopulationIncreaseMultiplier);
            empire.MaxNumberOfPopulation -= UpgradeEffectConstants.FuturisticResidentialArea_MaxPopulationIncrease;

            if (empire.Population > empire.MaxNumberOfPopulation)
            {
                empire.Population = empire.MaxNumberOfPopulation;
            }
        }

        public FuturisticResidentialAreaUpgrade()
        {
            Name = UpgradeEnum.FuturisticResidentialArea.GetDisplayName();
            Description = UpgradeDescriptionConstants.FuturisticResidentialArea_Description;
            UpgradeType = UpgradeTypeConstants.FuturisticResidentialAreaType;
            UpgradeTime = TimeConstants.UpgradeTime;
        }
    }
}
