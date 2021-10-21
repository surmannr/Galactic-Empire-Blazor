using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Unit
{
    public class BattleUnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Count { get; set; }
        public string ImageUrl { get; set; }
        public int AttackPoint { get; set; }
        public int DefensePoint { get; set; }
    }
}
