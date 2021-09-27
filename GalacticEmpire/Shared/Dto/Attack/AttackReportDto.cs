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
        public DateTimeOffset Date { get; set; }
        public string AttackedEmpireName { get; set; }
        public ICollection<BattleUnitDto> AttackUnits { get; set; }
        public ICollection<BattleUnitDto> DefenseUnits { get; set; }
        public Guid? WinnerEmpireId { get; set; }
    }
}
