using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.EventModel.Base;
using GalacticEmpire.Shared.Constants.Event;
using GalacticEmpire.Shared.Enums.Event;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.EventModel
{
    public class SatisfiedUnitsEvent : Event
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            foreach(var unit in empire.EmpireUnits)
            {
                unit.FightPoint.AttackPointMultiplier += EventEffectConstants.SatisfiedUnits_AttackAndDefenseMultiplier;
            }
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            foreach (var unit in empire.EmpireUnits)
            {
                unit.FightPoint.AttackPointMultiplier -= EventEffectConstants.SatisfiedUnits_AttackAndDefenseMultiplier;
            }
        }

        public SatisfiedUnitsEvent()
        {
            Name = EventEnum.SatisfiedUnits.GetDisplayName();
            Description = EventDescriptionConstants.SatisfiedUnits_Description;
            EventType = EventTypeConstants.SatisfiedUnitsType;
            ImageUrl = @"https://galacticempire.blob.core.windows.net/eventimages/satisfied_units.jpg";
        }
    }
}
