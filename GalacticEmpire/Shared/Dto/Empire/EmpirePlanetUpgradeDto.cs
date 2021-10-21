using GalacticEmpire.Shared.Dto.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Empire
{
    public class EmpirePlanetUpgradeDto
    {
        public int UpgradeId { get; set; }
        public string UpgradeName { get; set; }
        public string UpgradeDescription { get; set; }
        public string ImageUrl { get; set; }
    }
}
