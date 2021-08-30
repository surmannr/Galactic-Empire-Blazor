using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.UnitModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.EmpireModel
{
    public class EmpireUnit
    {
        public int Amount { get; set; }
        public int Level { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public Guid EmpireId { get; set; }
        public Empire Empire { get; set; }
        
        public FightPoint FightPoint { get; set; }
    }
}
