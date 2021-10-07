using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Drone.Events
{
    public class DroneTimingEvent : INotification
    {
        public Domain.Models.AttackModel.DroneAttack DroneAttack { get; set; }
    }
}
