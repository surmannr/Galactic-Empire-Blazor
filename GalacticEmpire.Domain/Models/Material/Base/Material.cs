using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Domain.Models.PlanetModel;
using GalacticEmpire.Domain.Models.UnitModel;
using GalacticEmpire.Domain.Models.UpgradeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.MaterialModel.Base
{
    public class Material : BaseModel<int>
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<UnitPriceMaterial> UnitPriceMaterials { get; set; }
        public ICollection<PlanetPriceMaterial> PlanetPriceMaterials { get; set; }
        public ICollection<UpgradePriceMaterial> UpgradePriceMaterials { get; set; }
        public ICollection<EmpireMaterial> EmpireMaterials { get; set; }
    }
}
