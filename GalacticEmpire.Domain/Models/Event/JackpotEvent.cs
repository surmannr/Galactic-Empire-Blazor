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
    public class JackpotEvent : Event
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            foreach (var material in empire.EmpireMaterials)
            {
                material.Amount += EventEffectConstants.Jackpot;
            }
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            foreach (var material in empire.EmpireMaterials)
            {
                material.Amount -= EventEffectConstants.Jackpot;
            }
        }

        public JackpotEvent()
        {
            Name = EventEnum.Jackpot.GetDisplayName();
            Description = EventDescriptionConstants.Jackpot_Description;
            EventType = EventTypeConstants.JackpotType;
        }
    }
}
