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
    public class YoiphusPlanet : Planet
    {
        public override void ApplyEffect(Empire empire)
        {
            base.ApplyEffect(empire);
        }

        public override void RemoveEffect(Empire empire)
        {
            base.RemoveEffect(empire);
        }

        public YoiphusPlanet()
        {
            Name = PlanetEnum.Yoiphus.GetDisplayName();
            Description = PlanetDescriptionConstants.Yoiphus_Description;
            PlanetType = PlanetTypeConstants.YoiphusType;
            CapturingTime = TimeConstants.PlanetCaptureTime;
            PlanetProperty = new PlanetProperty
            {
                BaseFood = PlanetEffectConstants.Yoiphus_BaseFood,
                BaseBitcoin = PlanetEffectConstants.Yoiphus_BaseBitcoin,
                BaseQuartz = PlanetEffectConstants.Yoiphus_BaseQuartz,
                MaxPopulationCount = PlanetEffectConstants.Yoiphus_MaxPopulationCount,
                MaxUnitCount = PlanetEffectConstants.Yoiphus_MaxUnitCount
            };
        }
    }
}
