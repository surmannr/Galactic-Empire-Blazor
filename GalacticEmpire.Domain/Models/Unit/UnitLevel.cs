using GalacticEmpire.Domain.Models.UnitModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.UnitModel
{
    public class UnitLevel
    {
        public int Level { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public int AttackPoint { get; set; }
        public int DefensePoint { get; set; }

        public double AttackPointMultiplier { get; set; }
        public double DefensePointMultiplier { get; set; }

        public int AttackPointBonus { get; set; }
        public int DefensePointBonus { get; set; }

        public TimeSpan TrainingTime { get; set; }
    }
}
