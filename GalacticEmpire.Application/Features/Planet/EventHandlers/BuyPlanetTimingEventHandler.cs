using GalacticEmpire.Application.Features.Planet.Event;
using GalacticEmpire.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Planet.EventHandlers
{
    public class BuyPlanetTimingEventHandler : INotificationHandler<BuyPlanetTimingEvent>
    {
        private readonly GalacticEmpireDbContext dbContext;

        public BuyPlanetTimingEventHandler(GalacticEmpireDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(BuyPlanetTimingEvent notification, CancellationToken cancellationToken)
        {
            var planet = await dbContext.Planets
                    .Where(p => p.Id == notification.PlanetId)
                    .Include(e => e.PlanetPriceMaterials)
                    .Include(e => e.PlanetProperty)
                    .SingleOrDefaultAsync();

            var empire = await dbContext.Empires
                    .Where(e => e.Id == notification.EmpireId)
                    .Include(e => e.EmpireMaterials)
                        .ThenInclude(e => e.Material)
                    .Include(e => e.EmpirePlanets)
                        .ThenInclude(e => e.Planet)
                            .ThenInclude(e => e.PlanetPriceMaterials)
                    .SingleAsync();

            empire.EmpirePlanets.Add(
                new Domain.Models.EmpireModel.EmpirePlanet()
                {
                    EmpireId = notification.EmpireId,
                    PlanetId = notification.PlanetId
                }
            );

            planet.ApplyEffect(empire);

            var activeCapturing = await dbContext.ActiveCapturings.FirstOrDefaultAsync(a => a.EmpireId == notification.EmpireId);

            if(activeCapturing != null)
            {
                dbContext.ActiveCapturings.Remove(activeCapturing);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}