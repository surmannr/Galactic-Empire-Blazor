using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.PlanetModel.Base;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GalacticEmpire.Domain.Models.EmpireModel
{
    public class EmpirePlanet
    {
        public Guid PlanetId { get; set; }
        public Planet Planet { get; set; }

        public Guid EmpireId { get; set; }
        public Empire Empire { get; set; }

        public ICollection<EmpirePlanetUpgrade> EmpirePlanetUpgrades { get; set; }
    }
}
