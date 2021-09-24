using GalacticEmpire.Domain.Models.EmpireModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.AllianceModel.Base
{
    public class Alliance : BaseModel<Guid>
    {
        public string Name { get; set; }

        public ICollection<AllianceMember> Members { get; set; }
        public ICollection<AllianceInvitation> AllianceInvitations { get; set; }
    }
}
