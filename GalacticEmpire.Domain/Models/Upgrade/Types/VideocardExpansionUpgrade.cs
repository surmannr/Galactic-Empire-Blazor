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
    public class VideocardExpansionUpgrade : Upgrade
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            var bitcoinMaterial = empire.EmpireMaterials
                .FirstOrDefault(e => e.Material.Name == MaterialEnum.Bitcoin.GetDisplayName());

            bitcoinMaterial.ProductionMultiplier += UpgradeEffectConstants.VideocardExpansion_BitcoinMultiplier;
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            var bitcoinMaterial = empire.EmpireMaterials
                 .FirstOrDefault(e => e.Material.Name == MaterialEnum.Bitcoin.GetDisplayName());

            bitcoinMaterial.ProductionMultiplier -= UpgradeEffectConstants.VideocardExpansion_BitcoinMultiplier;
        }

        public VideocardExpansionUpgrade()
        {
            Name = UpgradeEnum.VideocardExpansion.GetDisplayName();
            Description = UpgradeDescriptionConstants.VideocardExpansion_Description;
            UpgradeType = UpgradeTypeConstants.VideocardExpansionType;
            UpgradeTime = TimeConstants.UpgradeTime;
        }
    }
}
