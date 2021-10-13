using GalacticEmpire.Application.Features.Drone.Events;
using GalacticEmpire.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Drone.EventHandlers
{
    public class DroneTimingEventHandler : INotificationHandler<DroneTimingEvent>
    {
        private readonly GalacticEmpireDbContext dbContext;

        public DroneTimingEventHandler(GalacticEmpireDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(DroneTimingEvent notification, CancellationToken cancellationToken)
        {
            dbContext.DroneAttacks.Add(notification.DroneAttack);

            var activeSpying = await dbContext.ActiveSpyings.FirstOrDefaultAsync(a => a.EmpireId == notification.DroneAttack.AttackerId);

            if (activeSpying != null)
            {
                dbContext.ActiveSpyings.Remove(activeSpying);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}