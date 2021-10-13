using GalacticEmpire.Application.Features.Upgrade.Events;
using GalacticEmpire.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Upgrade.EventHandlers
{
    public class UpgradeTimingEventHandler : INotificationHandler<UpgradeTimingEvent>
    {
        private readonly GalacticEmpireDbContext dbContext;

        public UpgradeTimingEventHandler(GalacticEmpireDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(UpgradeTimingEvent notification, CancellationToken cancellationToken)
        {
            var empirePlanet = await dbContext.EmpirePlanets
                .Where(ep => ep.Id == notification.EmpirePlanetId)
                .FirstOrDefaultAsync();

            var upgrade = await dbContext.Upgrades
                .SingleOrDefaultAsync(t => t.Id == notification.UpgradeId);

            var empire = await dbContext.Empires
                    .Where(e => e.Id == notification.EmpireId)
                    .Include(e => e.EmpireMaterials)
                        .ThenInclude(e => e.Material)
                    .Include(e => e.EmpirePlanets)
                        .ThenInclude(e => e.EmpirePlanetUpgrades)
                            .ThenInclude(e => e.Upgrade)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.FightPoint)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.Unit)
                    .SingleAsync();

            empirePlanet.EmpirePlanetUpgrades.Add(
                new Domain.Models.EmpireModel.EmpirePlanetUpgrade()
                {
                    EmpirePlanetId = notification.EmpirePlanetId,
                    UpgradeId = notification.UpgradeId
                }
            );

            upgrade.ApplyEffect(empire);

            var activeUpgrading = await dbContext.ActiveUpgradings.FirstOrDefaultAsync(a => a.EmpireId == notification.EmpireId);

            if (activeUpgrading != null)
            {
                dbContext.ActiveUpgradings.Remove(activeUpgrading);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}