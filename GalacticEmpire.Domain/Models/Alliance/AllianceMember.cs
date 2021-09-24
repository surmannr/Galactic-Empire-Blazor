using GalacticEmpire.Domain.Models.AllianceModel.Base;
using GalacticEmpire.Domain.Models.EmpireModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.AllianceModel
{
    public class AllianceMember
    {
        public Guid AllianceId { get; set; }
        public Alliance Alliance { get; set; }

        public Guid EmpireId { get; set; }
        public Empire Empire { get; set; }

        public bool InvitationRight { get; set; }
        public bool IsLeader { get; set; }
    }
}
