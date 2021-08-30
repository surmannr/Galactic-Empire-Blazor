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
    public class QuartzMineUpgrade : Upgrade
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            var quartzMaterial = empire.EmpireMaterials
                .FirstOrDefault(e => e.Material.Name == MaterialEnum.Quartz.GetDisplayName());

            quartzMaterial.ProductionMultiplier += UpgradeEffectConstants.QuartzMine_QuartzMultiplier;
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            var quartzMaterial = empire.EmpireMaterials
                .FirstOrDefault(e => e.Material.Name == MaterialEnum.Quartz.GetDisplayName());

            quartzMaterial.ProductionMultiplier -= UpgradeEffectConstants.QuartzMine_QuartzMultiplier;
        }

        public QuartzMineUpgrade()
        {
            Name = UpgradeEnum.QuartzMine.GetDisplayName();
            Description = UpgradeDescriptionConstants.QuartzMine_Description;
            UpgradeType = UpgradeTypeConstants.QuartzMineType;
            UpgradeTime = TimeConstants.UpgradeTime;
        }
    }
}
