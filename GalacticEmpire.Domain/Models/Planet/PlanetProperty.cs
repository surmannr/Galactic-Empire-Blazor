using GalacticEmpire.Domain.Models.PlanetModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.PlanetModel
{
    public class PlanetProperty
    {
        public int PlanetId { get; set; }
        public Planet Planet { get; set; }

        public int BaseFood { get; set; } = 0;
        public int BaseQuartz { get; set; } = 0;
        public int BaseBitcoin { get; set; } = 0;
        public int MaxUnitCount { get; set; } = 0;
        public int MaxPopulationCount { get; set; } = 0;
    }
}
