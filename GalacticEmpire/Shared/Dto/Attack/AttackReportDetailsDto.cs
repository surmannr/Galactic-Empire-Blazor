using GalacticEmpire.Shared.Dto.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Attack
{
    public class AttackReportDetailsDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string OpponentEmpireName { get; set; }
        public ICollection<BattleUnitDto> AttackUnits { get; set; }
        public ICollection<BattleUnitDto> DefenseUnits { get; set; }
        public Guid? WinnerEmpireId { get; set; }
        public string WinnerEmpireName { get; set; }
    }
}
