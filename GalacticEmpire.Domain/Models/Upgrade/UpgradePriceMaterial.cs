using GalacticEmpire.Domain.Models.MaterialModel.Base;
using GalacticEmpire.Domain.Models.UpgradeModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.UpgradeModel
{
    public class UpgradePriceMaterial
    {
        public int Amount { get; set; }

        public Guid MaterialId { get; set; }
        public Material Material { get; set; }

        public Guid UpgradeId { get; set; }
        public Upgrade Upgrade { get; set; }
    }
}
