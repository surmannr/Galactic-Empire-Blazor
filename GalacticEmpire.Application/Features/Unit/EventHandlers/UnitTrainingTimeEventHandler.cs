using GalacticEmpire.Application.Features.Unit.Events;
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

namespace GalacticEmpire.Application.Features.Unit.EventHandlers
{
    public class UnitTrainingTimeEventHandler : INotificationHandler<UnitTrainingTimeEvent>
    {
        private readonly GalacticEmpireDbContext dbContext;
        private readonly IGameHubService gameHubService;

        public UnitTrainingTimeEventHandler(GalacticEmpireDbContext dbContext, IGameHubService gameHubService)
        {
            this.dbContext = dbContext;
            this.gameHubService = gameHubService;
        }

        public async Task Handle(UnitTrainingTimeEvent notification, CancellationToken cancellationToken)
        {
            var empire = await dbContext.Empires
                .Include(e => e.EmpireUnits)
                .FirstOrDefaultAsync(e => e.Id == notification.EmpireId);

            foreach (var buyUnit in notification.UnitsCollection.Units)
            {
                var empireUnit = empire.EmpireUnits
                    .Where(e => e.UnitId == buyUnit.UnitId && e.Level == buyUnit.Level)
                    .FirstOrDefault();

                if (empireUnit == null)
                {
                    throw new Exception("Nem létezik ilyen egység!");
                }

                empireUnit.Amount += buyUnit.Count;
            }

            var activeTrainings = await dbContext.ActiveTrainings.Where(a => a.EmpireId == notification.EmpireId).ToListAsync();

            if (activeTrainings != null)
            {
                dbContext.ActiveTrainings.RemoveRange(activeTrainings);
            }

            await dbContext.SaveChangesAsync();

            await gameHubService.FinishJob(notification.ConnectionId, "Az egységeid képzésének vége.");
        }
    }
}