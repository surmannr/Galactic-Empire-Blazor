using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Attack
{
    public class AttackReportPreviewDto
    {
        public Guid Id {  get; set; }
        public DateTimeOffset Date { get; set; }
        public string OpponentEmpireName { get; set; }
        public Guid? WinnerEmpireId { get; set; }
        public string WinnerEmpireName { get; set; }
        public int? DefensePoints { get; set; }
    }
}
