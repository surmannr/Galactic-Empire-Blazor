using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Alliance
{
    public class SendAllianceInvitationDto
    {
        public Guid AllianceId { get; set; }
        public Guid InvitedEmpireId { get; set; }
    }
}
