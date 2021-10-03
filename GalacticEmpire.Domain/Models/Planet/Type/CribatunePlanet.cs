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

        public CribatunePlanet(int id)
        {
            Id = id;
            Name = PlanetEnum.Cribatune.GetDisplayName();
            Description = PlanetDescriptionConstants.Cribatune_Description;
            PlanetType = PlanetTypeConstants.CribatuneType;
            CapturingTime = TimeConstants.PlanetCaptureTime;
            ImageUrl = @"https://galacticempire.blob.core.windows.net/planetimages/Cribatune.png";
        }
    }
}
