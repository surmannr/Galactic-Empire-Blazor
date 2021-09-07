using GalacticEmpire.Shared.Dto.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Attack
{
    public class AttackReportDto
    {
        public string AttackedEmpireName { get; set; }
        public ICollection<BattleUnitDto> Units { get; set; }
        public Guid? WinnerEmpireId { get; set; }
    }
}
