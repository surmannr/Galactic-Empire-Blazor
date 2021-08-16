using GalacticEmpire.Domain.Models.AttackModel.Base;
using GalacticEmpire.Domain.Models.UnitModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.AttackModel
{
    public class AttackUnit
    {
        public int Level { get; set; }
        public int Amount { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public Guid AttackId { get; set; }
        public Attack Attack { get; set; }
    }
}
