using GalacticEmpire.Domain.Models.MaterialModel.Base;
using GalacticEmpire.Domain.Models.PlanetModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.PlanetModel
{
    public class PlanetPriceMaterial
    {
        public int Amount { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int PlanetId { get; set; }
        public Planet Planet { get; set; }
    }
}
