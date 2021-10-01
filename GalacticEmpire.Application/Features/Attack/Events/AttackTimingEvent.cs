using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Attack.Events
{
    public class AttackTimingEvent : INotification
    {
        public Domain.Models.AttackModel.Base.Attack Attack { get; set; }
    }
}
