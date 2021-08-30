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
    public class DillonPlanet : Planet
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);
        }

        public DillonPlanet()
        {
            Name = PlanetEnum.Dillon.GetDisplayName();
            Description = PlanetDescriptionConstants.Dillon_Description;
            PlanetType = PlanetTypeConstants.DillonType;
            CapturingTime = TimeConstants.PlanetCaptureTime;
            PlanetProperty = new PlanetProperty
            {
                BaseFood = PlanetEffectConstants.Dillon_BaseFood,
                BaseBitcoin = PlanetEffectConstants.Dillon_BaseBitcoin,
                BaseQuartz = PlanetEffectConstants.Dillon_BaseQuartz,
                MaxPopulationCount = PlanetEffectConstants.Dillon_MaxPopulationCount,
                MaxUnitCount = PlanetEffectConstants.Dillon_MaxUnitCount
            };
        }
    }
}
