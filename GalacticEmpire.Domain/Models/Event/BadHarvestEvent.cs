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
    public class BadHarvestEvent : Event
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);

            foreach(var material in empire.EmpireMaterials)
            {
                material.ProductionMultiplier += EventEffectConstants.BadHarvest_ProductionPercentage;
            }
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);

            foreach (var material in empire.EmpireMaterials)
            {
                material.ProductionMultiplier -= EventEffectConstants.BadHarvest_ProductionPercentage;
            }
        }

        public BadHarvestEvent()
        {
            Name = EventEnum.BadHarvest.GetDisplayName();
            Description = EventDescriptionConstants.BadHarvest_Description;
            EventType = EventTypeConstants.BadHarvestType;
        }
    }
}
