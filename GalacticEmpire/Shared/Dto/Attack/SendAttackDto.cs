using GalacticEmpire.Shared.Dto.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Attack
{
    public class SendAttackDto
    {
        public Guid AttackedEmpireId { get; set; }
        public ICollection<SendAttackUnitDto> Units { get; set; }
    }
}
