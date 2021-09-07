using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Unit
{
    public class SendAttackUnitDto
    {
        public int UnitId { get; set; }
        public int Level { get; set; }
        public int Count { get; set; }
    }
}
