using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.UpgradeModel.Base;
using GalacticEmpire.Shared.Constants.Time;
using GalacticEmpire.Shared.Constants.Upgrade;
using GalacticEmpire.Shared.Enums.Material;
using GalacticEmpire.Shared.Enums.Upgrade;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.UpgradeModel.Types
{
    public class InterdimensionalGastrogardenUpgrade : Upgrade
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            var foodMaterial = empire.EmpireMaterials
                .FirstOrDefault(e => e.Material.Name == MaterialEnum.Food.GetDisplayName());

            foodMaterial.ProductionMultiplier += UpgradeEffectConstants.InterdimensionalGastrogarden_FoodMultiplier;
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            var foodMaterial = empire.EmpireMaterials
                .FirstOrDefault(e => e.Material.Name == MaterialEnum.Food.GetDisplayName());

            foodMaterial.ProductionMultiplier -= UpgradeEffectConstants.InterdimensionalGastrogarden_FoodMultiplier;
        }

        public InterdimensionalGastrogardenUpgrade()
        {
            Name = UpgradeEnum.InterdimensionalGastrogarden.GetDisplayName();
            Description = UpgradeDescriptionConstants.InterdimensionalGastrogarden_Description;
            UpgradeType = UpgradeTypeConstants.InterdimensionalGastrogardenType;
            UpgradeTime = TimeConstants.UpgradeTime;
            ImageUrl = @"https://galacticempire.blob.core.windows.net/upgradeimages/interdimensional_garden.jpg";
        }
    }
}
