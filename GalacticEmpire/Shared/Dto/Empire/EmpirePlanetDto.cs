using GalacticEmpire.Shared.Dto.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Empire
{
    public class EmpirePlanetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public PlanetPropertyDto PlanetProperty { get; set; }
        public ICollection<EmpirePlanetUpgradeDto> Upgrades { get; set; }
    }
}
