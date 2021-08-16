using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.UpgradeModel.Base;
using System;

namespace GalacticEmpire.Domain.Models.EmpireModel
{
    public class EmpireUpgrade
    {
        public Guid UpgradeId { get; set; }
        public Upgrade Upgrade { get; set; }

        public Guid EmpireId { get; set; }
        public Empire Empire { get; set; }
    }
}
