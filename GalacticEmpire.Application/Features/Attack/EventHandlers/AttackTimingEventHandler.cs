using GalacticEmpire.Application.Features.Attack.Events;
using GalacticEmpire.Application.SignalR;
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
        private readonly IGameHubService gameHubService;

        public AttackTimingEventHandler(GalacticEmpireDbContext dbContext, IGameHubService gameHubService)
        {
            this.dbContext = dbContext;
            this.gameHubService = gameHubService;
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

            if (notification.Attack.WinnerId == notification.Attack.AttackerId)
            {
                await gameHubService.FinishJob(notification.ConnectionId, $"A támadás sikeresen volt, megnyerted a csatát!");
            }
            else
            {
                await gameHubService.FinishJob(notification.ConnectionId, $"A csatát elvesztetted, a támadásod kudarcba fulladt.");
            }
        }
    }
}