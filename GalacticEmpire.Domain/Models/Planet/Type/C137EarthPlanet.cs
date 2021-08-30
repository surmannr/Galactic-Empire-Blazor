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
    public class C137EarthPlanet : Planet
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);
        }

        public C137EarthPlanet()
        {
            Name = PlanetEnum.C137Earth.GetDisplayName();
            Description = PlanetDescriptionConstants.C137Earth_Description;
            PlanetType = PlanetTypeConstants.C137EarthType;
            CapturingTime = TimeConstants.PlanetCaptureTime;
            PlanetProperty = new PlanetProperty
            {
                BaseFood = PlanetEffectConstants.C137Earth_BaseFood,
                BaseBitcoin = PlanetEffectConstants.C137Earth_BaseBitcoin,
                BaseQuartz = PlanetEffectConstants.C137Earth_BaseQuartz,
                MaxPopulationCount = PlanetEffectConstants.C137Earth_MaxPopulationCount,
                MaxUnitCount = PlanetEffectConstants.C137Earth_MaxUnitCount
            };
        }
    }
}
