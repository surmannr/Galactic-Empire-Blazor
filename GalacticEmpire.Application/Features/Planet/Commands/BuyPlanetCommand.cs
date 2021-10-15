using AutoMapper;
using FluentValidation;
using FluentValidation.Validators;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.Features.Planet.Event;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Domain.Models.Activities;
using GalacticEmpire.Shared.Constants.Time;
using GalacticEmpire.Shared.Dto.Planet;
using GalacticEmpire.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Planet.Commands
{
    public static class BuyPlanetCommand
    {
        public class Command : ICommand<bool>
        {
            public BuyPlanetDto BuyPlanet { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IIdentityService identityService;
            private readonly IMediator mediator;

            public Handler(GalacticEmpireDbContext dbContext, IIdentityService identityService, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.identityService = identityService;
                this.mediator = mediator;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Where(e => e.OwnerId == userId)
                    .Include(e => e.EmpireMaterials)
                        .ThenInclude(e => e.Material)
                    .Include(e => e.EmpirePlanets)
                        .ThenInclude(e => e.Planet)
                            .ThenInclude(e => e.PlanetPriceMaterials)
                    .SingleAsync();

                var active = await dbContext.ActiveCapturings.FirstOrDefaultAsync(a => a.EmpireId == empire.Id);

                if(active != null)
                {
                    throw new InProcessException("Folyamatban van egy bolygófoglalás.");
                }

                if (empire.EmpirePlanets.Any(e => e.Planet.Id == request.BuyPlanet.PlanetId))
                {
                    throw new InvalidActionException("Már van ilyen bolygód.");
                }

                var planet = await dbContext.Planets
                    .Where(p => p.Id == request.BuyPlanet.PlanetId)
                    .Include(e => e.PlanetPriceMaterials)
                    .Include(e => e.PlanetProperty)
                    .SingleOrDefaultAsync();

                if(planet == null)
                {
                    throw new NotFoundException("Nem létezik ilyen bolygó.");
                }

                foreach(var material in planet.PlanetPriceMaterials)
                {
                    var empireMaterial = empire.EmpireMaterials.SingleOrDefault(em => em.MaterialId == material.MaterialId);
                    
                    if(empireMaterial != null)
                    {
                        empireMaterial.Amount -= material.Amount;

                        if(empireMaterial.Amount < 0)
                        {
                            throw new InvalidActionException("Nincs elegendő nyersanyag!");
                        }
                    }
                }

                var activeCapturing = new ActiveCapturing
                {
                    EmpireId = empire.Id,
                    EndDate = DateTimeOffset.Now.Add(planet.CapturingTime),
                    PlanetName = planet.Name
                };

                dbContext.ActiveCapturings.Add(activeCapturing);

                await dbContext.SaveChangesAsync();

                mediator.Schedule(new BuyPlanetTimingEvent { EmpireId = empire.Id, PlanetId = planet.Id }, planet.CapturingTime);

                return true;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.BuyPlanet).NotNull().SetValidator(new BuyPlanetValidator());
            }

            public class BuyPlanetValidator : AbstractValidator<BuyPlanetDto>
            {
                public BuyPlanetValidator()
                {
                    RuleFor(x => x.PlanetId).NotNull().WithMessage("A PlanetId nem maradhat üresen.");
                }
            }
        }
    }
}
