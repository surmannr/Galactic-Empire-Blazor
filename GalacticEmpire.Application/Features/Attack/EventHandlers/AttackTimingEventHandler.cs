using GalacticEmpire.Application.Features.Attack.Events;
using GalacticEmpire.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

            var activeAttacking = await dbContext.ActiveAttackings.FirstOrDefaultAsync(a => a.EmpireId == notification.Attack.AttackerId);

            if (activeAttacking != null)
            {
                dbContext.ActiveAttackings.Remove(activeAttacking);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}