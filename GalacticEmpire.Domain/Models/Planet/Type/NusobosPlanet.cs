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
    public class NusobosPlanet : Planet
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);
        }

        public NusobosPlanet()
        {
            Name = PlanetEnum.Nusobos.GetDisplayName();
            Description = PlanetDescriptionConstants.Nusobos_Description;
            PlanetType = PlanetTypeConstants.NusobosType;
            CapturingTime = TimeConstants.PlanetCaptureTime;
            PlanetProperty = new PlanetProperty
            {
                BaseFood = PlanetEffectConstants.Nusobos_BaseFood,
                BaseBitcoin = PlanetEffectConstants.Nusobos_BaseBitcoin,
                BaseQuartz = PlanetEffectConstants.Nusobos_BaseQuartz,
                MaxPopulationCount = PlanetEffectConstants.Nusobos_MaxPopulationCount,
                MaxUnitCount = PlanetEffectConstants.Nusobos_MaxUnitCount
            };
        }
    }
}
