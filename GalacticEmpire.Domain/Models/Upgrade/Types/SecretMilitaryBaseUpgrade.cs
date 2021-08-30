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
    public class SecretMilitaryBaseUpgrade : Upgrade
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            empire.MaxNumberOfUnits += UpgradeEffectConstants.SecretMilitaryBase_UnitCount;
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            empire.MaxNumberOfUnits -= UpgradeEffectConstants.SecretMilitaryBase_UnitCount;
        }

        public SecretMilitaryBaseUpgrade()
        {
            Name = UpgradeEnum.SecretMilitaryBase.GetDisplayName();
            Description = UpgradeDescriptionConstants.SecretMilitaryBase_Description;
            UpgradeType = UpgradeTypeConstants.SecretMilitaryBaseType;
            UpgradeTime = TimeConstants.UpgradeTime;
        }
    }
}
