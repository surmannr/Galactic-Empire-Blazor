using AutoMapper;
using FluentValidation;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Material;
using GalacticEmpire.Shared.Dto.Time;
using GalacticEmpire.Shared.Dto.Upgrade;
using GalacticEmpire.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Upgrade.Queries
{
    public static class GetAvailableUpgradesForPlanet
    {
        public class Query : IRequest<List<UpgradeDto>>
        {
            public Guid EmpirePlanetId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<UpgradeDto>>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<List<UpgradeDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var empirePlanet = await dbContext.EmpirePlanets
                    .Include(e => e.EmpirePlanetUpgrades)
                        .ThenInclude(e => e.Upgrade)
                            .ThenInclude(e => e.UpgradePriceMaterials)
                                .ThenInclude(e => e.Material)
                    .FirstOrDefaultAsync(c => c.Id == request.EmpirePlanetId);

                if (empirePlanet == null)
                {
                    throw new NotFoundException("Nincs ilyen bolygója a birodalomnak.");
                }

                var empirePlanetUpgrades = empirePlanet.EmpirePlanetUpgrades.Select(e => e.Upgrade).ToList();

                var upgrades = await dbContext.Upgrades
                    .Include(e => e.UpgradePriceMaterials)
                        .ThenInclude(e => e.Material)
                    .ToListAsync();

                return upgrades.Except(empirePlanetUpgrades).Select(c =>
                    new UpgradeDto { 
                        Id = c.Id,
                        Description = c.Description,
                        Name = c.Name,
                        ImageUrl = c.ImageUrl,
                        RequiredMaterials = c.UpgradePriceMaterials.Select(a => new PriceMaterialDto { 
                            Id = a.MaterialId,
                            Amount = a.Amount,
                            ImageUrl = a.Material.ImageUrl,
                            Name = a.Material.Name
                        }).ToList(),
                        UpgradeTime = mapper.Map<TimeDto>(c.UpgradeTime),
                        DoesExist = empirePlanetUpgrades.Any(s => s.Id == c.Id)
                }).ToList();
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {

            }
        }
    }
}
