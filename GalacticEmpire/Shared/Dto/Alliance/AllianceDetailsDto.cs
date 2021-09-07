using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Alliance
{
    public class AllianceDetailsDto
    {
        public string Name { get; set; }
        public Guid LeaderEmpireId { get; set; }

        public ICollection<AllianceMemberDto> Members { get; set; }
    }
}
