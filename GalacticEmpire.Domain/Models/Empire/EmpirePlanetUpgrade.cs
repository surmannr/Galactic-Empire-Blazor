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
        public Guid UpgradeId { get; set; }
        public Upgrade Upgrade { get; set; }

        public Guid EmpireId { get; set; }
        public Planet Planet { get; set; }
    }
}
