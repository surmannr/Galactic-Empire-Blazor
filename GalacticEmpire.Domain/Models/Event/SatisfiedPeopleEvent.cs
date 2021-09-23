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
    public class SatisfiedPeopleEvent : Event
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            empire.Population += EventEffectConstants.SatisfiedPeople_Population;
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);
            
            // Ha kéne, de az esemény vége nem fogja módosítani
            // empire.Population -= EventEffectConstants.SatisfiedPeople_Population;
        }

        public SatisfiedPeopleEvent()
        {
            Name = EventEnum.SatisfiedPeople.GetDisplayName();
            Description = EventDescriptionConstants.SatisfiedPeople_Description;
            EventType = EventTypeConstants.SatisfiedPeopleType;
        }
    }
}
