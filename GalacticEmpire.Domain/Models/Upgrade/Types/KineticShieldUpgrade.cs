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
    public class KineticShieldUpgrade : Upgrade
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            foreach(var unit in empire.EmpireUnits)
            {
                unit.FightPoint.DefensePointMultiplier += UpgradeEffectConstants.KineticShield_DefenseMultiplier;
            }
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            foreach (var unit in empire.EmpireUnits)
            {
                unit.FightPoint.DefensePointMultiplier -= UpgradeEffectConstants.KineticShield_DefenseMultiplier;
            }
        }

        public KineticShieldUpgrade()
        {
            Name = UpgradeEnum.KineticShield.GetDisplayName();
            Description = UpgradeDescriptionConstants.KineticShield_Description;
            UpgradeType = UpgradeTypeConstants.KineticShieldType;
            UpgradeTime = TimeConstants.UpgradeTime;
        }
    }
}
