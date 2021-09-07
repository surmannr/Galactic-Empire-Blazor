using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Alliance
{
    public class AllianceInvitationDto
    {
        public Guid AllianceId { get; set; }
        public string AllianceName { get; set; }
        public Guid InviterEmpireId { get; set; }
        public string InviterEmpireName { get; set; }
        public int MembersCount { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
