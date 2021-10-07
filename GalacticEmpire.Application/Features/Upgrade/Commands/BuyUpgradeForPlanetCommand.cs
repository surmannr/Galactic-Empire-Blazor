using AutoMapper;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.Features.Upgrade.Events;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Constants.Time;
using GalacticEmpire.Shared.Dto.Upgrade;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Upgrade.Commands
{
    public static class BuyUpgradeForPlanetCommand
    {
        public class Command : ICommand<bool>
        {
            public BuyUpgradeDto BuyUpgrade { get; set; }
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
                        .ThenInclude(e => e.EmpirePlanetUpgrades)
                            .ThenInclude(e => e.Upgrade)
                    .SingleAsync();

                if (!empire.EmpirePlanets.Any(e => e.Id == request.BuyUpgrade.EmpirePlanetId))
                {
                    throw new Exception("Ez a bolygó, amihez a fejlesztést vásárolnád nincsen a birodalmadban.");
                }

                var upgrade = await dbContext.Upgrades
                    .Where(p => p.Id == request.BuyUpgrade.UpgradeId)
                    .Include(e => e.UpgradePriceMaterials)
                    .SingleOrDefaultAsync();

                if (upgrade == null)
                {
                    throw new Exception("Nem létezik ilyen fejlesztés.");
                }

                var empirePlanet = empire.EmpirePlanets
                    .Single(e => e.Id == request.BuyUpgrade.EmpirePlanetId);

                if (empirePlanet.EmpirePlanetUpgrades.Any(e => e.Upgrade.Id == request.BuyUpgrade.UpgradeId))
                {
                    throw new Exception("Már van ilyen fejlesztés a kiválasztott bolygón!");
                }

                foreach (var material in upgrade.UpgradePriceMaterials)
                {
                    var empireMaterial = empire.EmpireMaterials.SingleOrDefault(em => em.MaterialId == material.MaterialId);

                    if (empireMaterial != null)
                    {
                        empireMaterial.Amount -= material.Amount;

                        if (empireMaterial.Amount < 0)
                        {
                            throw new Exception("Nincs elegendő nyersanyag!");
                        }
                    }
                }

                mediator.Schedule(new UpgradeTimingEvent { 
                    EmpireId = empire.Id,
                    EmpirePlanetId = request.BuyUpgrade.EmpirePlanetId,
                    UpgradeId = request.BuyUpgrade.UpgradeId }, upgrade.UpgradeTime
                );

                return true;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.BuyUpgrade).NotNull().SetValidator(new BuyUpgradeValidator());
            }

            public class BuyUpgradeValidator : AbstractValidator<BuyUpgradeDto>
            {
                public BuyUpgradeValidator()
                {
                    RuleFor(x => x.EmpirePlanetId).NotNull().WithMessage("A birodalom bolygó azonosítója nem lehet null érték.");
                    RuleFor(x => x.UpgradeId).NotNull().WithMessage("A fejlesztés azonosítója nem lehet null érték.");
                }
            }
        }
    }
}
