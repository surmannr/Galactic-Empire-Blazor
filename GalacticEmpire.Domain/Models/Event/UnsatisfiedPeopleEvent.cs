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
    public class UnsatisfiedPeopleEvent : Event
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            empire.Population += EventEffectConstants.UnsatisfiedPeople_Population;
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            // Nem kell elvenni, ha vége az eseménynek
            // empire.Population -= EventEffectConstants.UnsatisfiedPeople_Population;
        }

        public UnsatisfiedPeopleEvent()
        {
            Name = EventEnum.UnsatisfiedPeople.GetDisplayName();
            Description = EventDescriptionConstants.UnsatisfiedPeople_Description;
            EventType = EventTypeConstants.UnsatisfiedPeopleType;
            ImageUrl = @"https://galacticempire.blob.core.windows.net/eventimages/unsatisfied_people.jpg";
        }
    }
}
