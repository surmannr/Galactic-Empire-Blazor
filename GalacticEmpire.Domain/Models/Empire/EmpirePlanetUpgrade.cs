using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.PlanetModel.Base;
using GalacticEmpire.Domain.Models.UpgradeModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.EmpireModel
{
    public class EmpirePlanetUpgrade
    {
        public int UpgradeId { get; set; }
        public Upgrade Upgrade { get; set; }

        public EmpirePlanet EmpirePlanet { get; set; }
        public Guid EmpirePlanetId { get; set; }
    }
}
