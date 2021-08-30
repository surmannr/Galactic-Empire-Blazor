using GalacticEmpire.Domain.Models.AllianceModel.Base;
using GalacticEmpire.Domain.Models.EmpireModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.AllianceModel
{
    public class AllianceInvitation
    {
        public DateTimeOffset Date { get; set; }

        public Guid AllianceId { get; set; }
        public Alliance Alliance { get; set; }

        public Guid InviterEmpireId { get; set; }
        public Empire InviterEmpire { get; set; }

        public Guid InvitedEmpireId { get; set; }
        public Empire InvitedEmpire { get; set; }
    }
}
