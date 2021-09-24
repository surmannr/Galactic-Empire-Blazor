using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Unit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Unit.Commands
{
    public static class BuyUnitsCommand
    {
        public class Command : ICommand<bool>
        {
            public BuyUnitsCollectionDto UnitsCollection { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IIdentityService identityService;

            public Handler(GalacticEmpireDbContext dbContext, IIdentityService identityService)
            {
                this.dbContext = dbContext;
                this.identityService = identityService;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Include(e => e.EmpireMaterials)
                        .ThenInclude(e => e.Material)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.Unit)
                            .ThenInclude(e => e.UnitLevels)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.Unit)
                            .ThenInclude(e => e.UnitPriceMaterials)
                    .Where(e => e.OwnerId == userId)
                    .SingleOrDefaultAsync();

                var unitBuyCollection = request.UnitsCollection.Units;

                int maxCountOfUnits = unitBuyCollection.Max(e => e.Count) + empire.EmpireUnits.Max(e => e.Amount);

                if(empire.MaxNumberOfUnits -  maxCountOfUnits < 0)
                {
                    throw new Exception("A megvételre szánt egység mennyisége túl lépi a birodalom korlátját.");
                }

                foreach (var buyUnit in unitBuyCollection)
                {
                    var empireUnit = empire.EmpireUnits
                        .Where(e => e.UnitId == buyUnit.UnitId && e.Level == buyUnit.Level)
                        .FirstOrDefault();

                    if(empireUnit == null)
                    {
                        throw new Exception("Nem létezik ilyen egység!");
                    }

                    foreach (var material in empireUnit.Unit.UnitPriceMaterials)
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

                    empireUnit.Amount += buyUnit.Count;
                }

                await dbContext.SaveChangesAsync();

                return true;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UnitsCollection).NotNull().SetValidator(new UnitsCollectionValidator());
            }

            public class UnitsCollectionValidator : AbstractValidator<BuyUnitsCollectionDto>
            {
                public UnitsCollectionValidator()
                {
                    RuleForEach(x => x.Units).NotNull().SetValidator(new BuyUnitValidator());
                }

                public class BuyUnitValidator : AbstractValidator<BuyUnitDetailsDto>
                {
                    public BuyUnitValidator()
                    {
                        RuleFor(x => x.Count).GreaterThanOrEqualTo(0).WithMessage("Nem adhatsz meg 0-nál kevesebb mennyiséget.");
                        RuleFor(x => x.Level).GreaterThanOrEqualTo(0).WithMessage("Nem adhatsz meg 0-nál kevesebb szintet az egységhez.");
                        RuleFor(x => x.UnitId).GreaterThanOrEqualTo(0).WithMessage("Az azonosító nem lehet kisebb 0-nál.");
                    }
                }
            }
        }
    }
}
