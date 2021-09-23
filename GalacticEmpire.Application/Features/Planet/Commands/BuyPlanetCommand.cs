﻿using AutoMapper;
using FluentValidation;
using FluentValidation.Validators;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Planet;
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
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IIdentityService identityService, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.identityService = identityService;
                this.mapper = mapper;
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

                if (empire.EmpirePlanets.Any(e => e.Planet.Id == request.BuyPlanet.PlanetId))
                {
                    throw new Exception("Már van ilyen bolygód.");
                }

                var planet = await dbContext.Planets
                    .Where(p => p.Id == request.BuyPlanet.PlanetId)
                    .Include(e => e.PlanetPriceMaterials)
                    .Include(e => e.PlanetProperty)
                    .SingleOrDefaultAsync();

                if(planet == null)
                {
                    throw new Exception("Nem létezik ilyen bolygó.");
                }

                foreach(var material in planet.PlanetPriceMaterials)
                {
                    var empireMaterial = empire.EmpireMaterials.SingleOrDefault(em => em.MaterialId == material.MaterialId);
                    
                    if(empireMaterial != null)
                    {
                        empireMaterial.Amount -= material.Amount;

                        if(empireMaterial.Amount < 0)
                        {
                            throw new Exception("Nincs elegendő nyersanyag!");
                        }
                    }
                }

                empire.EmpirePlanets.Add(
                    new Domain.Models.EmpireModel.EmpirePlanet()
                    {
                        EmpireId = empire.Id,
                        PlanetId = planet.Id
                    }    
                );

                planet.ApplyEffect(empire);

                await dbContext.SaveChangesAsync();

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