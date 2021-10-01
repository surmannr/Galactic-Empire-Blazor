using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.UserModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.AttackModel.Base
{
    public class Attack : BaseAttackModel<Guid, Empire>
    {
        public ICollection<AttackUnit> AttackUnits { get; set; }
        public ICollection<DefenseUnit> DefenseUnits { get; set; }
    }
}
