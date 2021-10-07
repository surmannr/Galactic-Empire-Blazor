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
    public class UnsatisfiedUnitsEvent : Event
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            foreach (var unit in empire.EmpireUnits)
            {
                unit.FightPoint.AttackPointMultiplier += EventEffectConstants.UnsatisfiedUnits_AttackAndDefense;
            }
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            foreach (var unit in empire.EmpireUnits)
            {
                unit.FightPoint.AttackPointMultiplier -= EventEffectConstants.UnsatisfiedUnits_AttackAndDefense;
            }
        }

        public UnsatisfiedUnitsEvent()
        {
            Name = EventEnum.UnsatisfiedUnits.GetDisplayName();
            Description = EventDescriptionConstants.UnsatisfiedUnits_Description;
            EventType = EventTypeConstants.UnsatisfiedUnitsType;
            ImageUrl = @"https://galacticempire.blob.core.windows.net/eventimages/unsatisfied_units.jpg";
        }
    }
}
