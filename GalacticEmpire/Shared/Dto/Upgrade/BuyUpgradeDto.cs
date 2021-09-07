using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Upgrade
{
    public class BuyUpgradeDto
    {
        public int UpgradeId { get; set; }
        public Guid EmpirePlanetId { get; set; }
    }
}
