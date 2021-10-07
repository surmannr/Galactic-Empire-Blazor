using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Upgrade.Events
{
    public class UpgradeTimingEvent : INotification
    {
        public Guid EmpireId { get; set; }
        public Guid EmpirePlanetId { get; set; }
        public int UpgradeId { get; set; }
    }
}
