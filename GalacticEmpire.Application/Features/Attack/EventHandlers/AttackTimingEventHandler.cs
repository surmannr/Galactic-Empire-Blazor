using GalacticEmpire.Application.Features.Attack.Events;
using GalacticEmpire.Dal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Attack.EventHandlers
{
    public class AttackTimingEventHandler : INotificationHandler<AttackTimingEvent>
    {
        private readonly GalacticEmpireDbContext dbContext;

        public AttackTimingEventHandler(GalacticEmpireDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(AttackTimingEvent notification, CancellationToken cancellationToken)
        {
            dbContext.Attacks.Add(notification.Attack);

            await dbContext.SaveChangesAsync();
        }
    }
}