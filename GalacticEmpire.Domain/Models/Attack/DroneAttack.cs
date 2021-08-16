using GalacticEmpire.Domain.Models.AttackModel.Base;
using GalacticEmpire.Domain.Models.EmpireModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.AttackModel
{
    public class DroneAttack : BaseAttackModel<Guid, Empire>
    {
        public int NumberOfDrones { get; set; }
        public int? DefenderDefensivePoints { get; set; }
    }
}
