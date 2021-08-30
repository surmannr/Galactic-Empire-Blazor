using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.PlanetModel.Base;
using GalacticEmpire.Shared.Constants.Planet;
using GalacticEmpire.Shared.Constants.Time;
using GalacticEmpire.Shared.Enums.Planet;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.PlanetModel.Type
{
    public class CribatunePlanet : Planet
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);
        }

        public CribatunePlanet()
        {
            Name = PlanetEnum.Cribatune.GetDisplayName();
            Description = PlanetDescriptionConstants.Cribatune_Description;
            PlanetType = PlanetTypeConstants.CribatuneType;
            CapturingTime = TimeConstants.PlanetCaptureTime;
            PlanetProperty = new PlanetProperty
            {
                BaseFood = PlanetEffectConstants.Cribatune_BaseFood,
                BaseBitcoin = PlanetEffectConstants.Cribatune_BaseBitcoin,
                BaseQuartz = PlanetEffectConstants.Cribatune_BaseQuartz,
                MaxPopulationCount = PlanetEffectConstants.Cribatune_MaxPopulationCount,
                MaxUnitCount = PlanetEffectConstants.Cribatune_MaxUnitCount
            };
        }
    }
}
