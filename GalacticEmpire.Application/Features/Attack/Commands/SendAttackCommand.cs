using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.Features.Attack.Events;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Domain.Models.AttackModel;
using GalacticEmpire.Shared.Constants.Time;
using GalacticEmpire.Shared.Dto.Attack;
using GalacticEmpire.Shared.Dto.Unit;
using GalacticEmpire.Shared.Enums.Unit;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Attack.Commands
{
    public static class SendAttackCommand
    {
        public class Command : ICommand<bool>
        {
            public SendAttackDto SendAttackDto { get; set; }
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
                    .Include(e => e.EmpireMaterials)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.Unit)
                            .ThenInclude(e => e.UnitLevels)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.FightPoint)
                    .Where(e => e.OwnerId == userId)
                    .FirstOrDefaultAsync();

                var drone = await dbContext.Units
                    .Where(e => e.Name == UnitEnum.ScoutDrone.GetDisplayName())
                    .SingleOrDefaultAsync();

                if (request.SendAttackDto.Units.Any(u => u.UnitId == drone.Id))
                {
                    throw new Exception("Nem lehet támadásba küldeni a felfedező drónt.");
                }

                if (request.SendAttackDto.AttackedEmpireId == empire.Id)
                {
                    throw new Exception("Nem támadhatod meg magadat!");
                }

                var attackedEmpire = await dbContext.Empires
                    .Include(e => e.EmpireMaterials)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.Unit)
                            .ThenInclude(e => e.UnitLevels)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.FightPoint)
                    .Where(e => e.Id == request.SendAttackDto.AttackedEmpireId)
                    .SingleOrDefaultAsync();

                if (attackedEmpire is null)
                {
                    throw new Exception("Nincs ilyen birodalom, amit megtámadnál.");
                }

                AttackLogic(empire, request, attackedEmpire);

                return true;
            }

            public void AttackLogic(Domain.Models.EmpireModel.Base.Empire empire, Command request, Domain.Models.EmpireModel.Base.Empire attackedEmpire)
            {
                var attack = new Domain.Models.AttackModel.Base.Attack()
                {
                    AttackerId = empire.Id,
                    DefenderId = request.SendAttackDto.AttackedEmpireId,
                    Date = DateTimeOffset.Now,
                    AttackUnits = request.SendAttackDto.Units.Select(unit => {

                        var empireUnit = empire.EmpireUnits
                            .Where(e => e.UnitId == unit.UnitId && e.Level == unit.Level)
                            .SingleOrDefault();

                        if (empireUnit is null)
                        {
                            throw new Exception("Nincs ilyen egységed.");
                        }

                        if (unit.Count > empireUnit.Amount)
                        {
                            throw new Exception("Nem áll rendelkezésre ennyi egység.");
                        }

                        var attackUnit = new AttackUnit
                        {
                            Level = unit.Level,
                            UnitId = unit.UnitId,
                            Amount = unit.Count
                        };

                        return attackUnit;

                    }).ToList(),
                    DefenseUnits = attackedEmpire.EmpireUnits
                        .Where(unit => unit.Amount > 0)
                        .Select(unit => {

                        var attackUnit = new DefenseUnit
                        {
                            Level = unit.Level,
                            UnitId = unit.UnitId,
                            Amount = unit.Amount
                        };

                        return attackUnit;

                    }).ToList(),
                    WinnerId = CalculateWinner(empire,request,attackedEmpire)
                };

                mediator.Schedule(new AttackTimingEvent() { Attack = attack }, TimeConstants.AttackAndSpyingTime);
            }

            public Guid? CalculateWinner(Domain.Models.EmpireModel.Base.Empire empire, Command request, Domain.Models.EmpireModel.Base.Empire attackedEmpire)
            {
                double attackPoints = 0;
                double defensePoints = 0;

                var attackUnits = empire.EmpireUnits.Where(eu => request.SendAttackDto.Units.Any(sa => sa.UnitId == eu.UnitId && sa.Level == eu.Level)).ToList();
                var defenseUnits = attackedEmpire.EmpireUnits.Where(eu => eu.Amount > 0).ToList();

                foreach (var unit in attackUnits)
                {
                    var unitlevel = unit.Unit.UnitLevels.SingleOrDefault(ul => ul.Level == unit.Level && ul.UnitId == unit.UnitId);

                    if (unitlevel is null)
                    {
                        throw new Exception("Nincs ilyen szintje az egységnek.");
                    }

                    attackPoints += ((unitlevel.AttackPoint + unit.FightPoint.AttackPointBonus) * unit.Amount) * unit.FightPoint.AttackPointMultiplier;
                }

                foreach (var unit in defenseUnits)
                {
                    var unitlevel = unit.Unit.UnitLevels.SingleOrDefault(ul => ul.Level == unit.Level);

                    if (unitlevel is null)
                    {
                        throw new Exception("Nincs ilyen szintje az egységnek.");
                    }

                    defensePoints += ((unitlevel.DefensePoint + unit.FightPoint.DefensePointBonus) * unit.Amount) * unit.FightPoint.DefensePointMultiplier;
                }

                if (defensePoints == attackPoints)
                {
                    return null;
                }

                if (attackPoints < defensePoints)
                {
                    foreach (var unit in attackUnits)
                    {
                        unit.Amount = (int)Math.Round(unit.Amount * 0.9);
                    }

                    return attackedEmpire.Id;
                }
                else
                {
                    foreach (var unit in defenseUnits)
                    {
                        unit.Amount = (int)Math.Round(unit.Amount * 0.9);
                    }

                    foreach (var material in attackedEmpire.EmpireMaterials)
                    {
                        var empireMaterial = empire.EmpireMaterials.SingleOrDefault(cm => cm.MaterialId == material.MaterialId);

                        if (empireMaterial is null)
                        {
                            throw new Exception("Nincs ilyen nyersanyag.");
                        }

                        empireMaterial.Amount += material.Amount / 2;
                        material.Amount /= 2;
                    }

                    return empire.Id;
                }
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SendAttackDto).SetValidator(new SendAttackValidator());
            }

            public class SendAttackValidator : AbstractValidator<SendAttackDto>
            {
                public SendAttackValidator()
                {
                    RuleFor(x => x.AttackedEmpireId).NotNull().WithMessage("A támadott birodalom azonosítója nem lehet null.");
                    RuleFor(x => x.Units).NotNull().NotEmpty().WithMessage("Legalább 1 egységet el kell küldened támadni.");
                    RuleForEach(x => x.Units).SetValidator(new SendAttackUnitValidator());
                }

                public class SendAttackUnitValidator : AbstractValidator<SendAttackUnitDto>
                {
                    public SendAttackUnitValidator()
                    {
                        RuleFor(x => x.UnitId).NotNull().WithMessage("Az egység azonosítója nem lehet null.");
                        RuleFor(x => x.Count).GreaterThanOrEqualTo(1).WithMessage("Az egység mennyiségének minimum 1-nek kell lennie.");
                        RuleFor(x => x.Level).InclusiveBetween(1,3).WithMessage("Az egység szintjének 1 és 3 között kell lennie.");
                    }
                }
            }
        }
    }
}
