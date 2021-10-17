using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Planet.Event
{
    public class BuyPlanetTimingEvent : INotification
    {
        public int PlanetId { get; set; }
        public Guid EmpireId { get; set; }
        public string ConnectionId { get; set; }
    }
}
