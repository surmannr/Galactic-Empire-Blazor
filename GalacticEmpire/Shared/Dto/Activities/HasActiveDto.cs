using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Activities
{
    public class HasActiveDto
    {
        public DateTimeOffset? ActiveCapturingDate { get; set; }
        public DateTimeOffset? ActiveUpgradingDate { get; set; }
        public DateTimeOffset? ActiveAttackingDate { get; set; }
        public DateTimeOffset? ActiveSpyingDate { get; set; }
        public DateTimeOffset? ActiveTrainingDate { get; set; }
    }
}
