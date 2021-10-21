using GalacticEmpire.Shared.Dto.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Unit
{
    public class UnitLevelDto
    {
        public int AttackPoint { get; set; }
        public int DefensePoint { get; set; }
        public int Level { get; set; }
        public int CurrentCount { get; set; }
        public TimeDto TrainingTime { get; set; }
    }
}
