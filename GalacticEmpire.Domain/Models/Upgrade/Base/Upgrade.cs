using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Domain.Models.EmpireModel.Base;
using System;
using System.Collections.Generic;

namespace GalacticEmpire.Domain.Models.UpgradeModel.Base
{
    public class Upgrade : BaseModel<int>, IBaseEffect<Empire>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UpgradeType { get; set; }
        public TimeSpan UpgradeTime { get; set; }

        public ICollection<EmpirePlanetUpgrade> PlanetUpgrades { get; set; }
        public ICollection<UpgradePriceMaterial> UpgradePriceMaterials { get; set; }

        public virtual void ApplyEffect(Empire model) { }

        public virtual void RemoveEffect(Empire model) { }
    }
}
