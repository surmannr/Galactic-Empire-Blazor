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
    public class VibraniumArmorUpgrade : Upgrade
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);
            
            foreach (var unit in empire.EmpireUnits)
            {
                unit.FightPoint.AttackPointMultiplier += UpgradeEffectConstants.VibraniumArmor_AttackAndDefenseMultiplier;
                unit.FightPoint.DefensePointMultiplier += UpgradeEffectConstants.VibraniumArmor_AttackAndDefenseMultiplier;
            }
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            foreach (var unit in empire.EmpireUnits)
            {
                unit.FightPoint.AttackPointMultiplier -= UpgradeEffectConstants.VibraniumArmor_AttackAndDefenseMultiplier;
                unit.FightPoint.DefensePointMultiplier -= UpgradeEffectConstants.VibraniumArmor_AttackAndDefenseMultiplier;
            }
        }

        public VibraniumArmorUpgrade()
        {
            Name = UpgradeEnum.VibraniumArmor.GetDisplayName();
            Description = UpgradeDescriptionConstants.VibraniumArmor_Description;
            UpgradeType = UpgradeTypeConstants.VibraniumArmorType;
            UpgradeTime = TimeConstants.UpgradeTime;
        }
    }
}
