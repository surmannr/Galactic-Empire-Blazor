using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.Features.Drone.Events;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Shared.Constants.Time;
using GalacticEmpire.Shared.Dto.Drone;
using GalacticEmpire.Shared.Enums.Unit;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Drone.Commands
{
    public static class SendDroneAttackCommand
    {
        public class Command : ICommand<bool>
        {
            public SendDroneDto SendDrone { get; set; }
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

                if (request.SendDrone.DronedEmpireId == empire.Id)
                {
                    throw new Exception("Nem kémkedheted meg magadat!");
                }

                var attackedEmpire = await dbContext.Empires
                    .Include(e => e.EmpireMaterials)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.Unit)
                            .ThenInclude(e => e.UnitLevels)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.FightPoint)
                    .Where(e => e.Id == request.SendDrone.DronedEmpireId)
                    .SingleOrDefaultAsync();

                if (attackedEmpire is null)
                {
                    throw new Exception("Nincs ilyen birodalom, amit kémkedhetnél.");
                }

                DroneLogic(empire, request, attackedEmpire, drone);

                return true;
            }

            public void DroneLogic(Domain.Models.EmpireModel.Base.Empire empire, Command request, Domain.Models.EmpireModel.Base.Empire dronedEmpire, Domain.Models.UnitModel.Base.Unit drone)
            {
                var empireDroneUnit = empire.EmpireUnits
                    .Where(eu => eu.Level == 1 && eu.UnitId == drone.Id)
                    .SingleOrDefault();

                var dronedEmpireUnit = dronedEmpire.EmpireUnits
                    .Where(eu => eu.Level == 1 && eu.UnitId == drone.Id)
                    .SingleOrDefault();

                if (empireDroneUnit is null)
                {
                    throw new Exception("Nincsen ilyen típusú egységed!");
                }

                if (empireDroneUnit.Amount < request.SendDrone.NumberOfDrones)
                {
                    throw new Exception("Nincs elegendő drónod a kémkedéshez.");
                }

                var droneAttack = new Domain.Models.AttackModel.DroneAttack()
                {
                    AttackerId = empire.Id,
                    DefenderId = request.SendDrone.DronedEmpireId,
                    NumberOfAttackerDrones = request.SendDrone.NumberOfDrones,
                    NumberOfDefenderDrones = dronedEmpireUnit?.Amount ?? 0,
                    Date = DateTimeOffset.Now,
                    WinnerId = CalculateWinner(empireDroneUnit, request, dronedEmpireUnit),
                };

                droneAttack.DefenderDefensivePoints = CalculateOpponentDefensivePoints(empire, droneAttack.WinnerId, dronedEmpire.EmpireUnits);

                mediator.Schedule(new DroneTimingEvent() { DroneAttack = droneAttack }, TimeConstants.AttackAndSpyingTime);
            }

            public Guid? CalculateWinner(EmpireUnit attackerDroneUnit, Command request, EmpireUnit? defenderDroneUnit)
            {
                double attackPoints = 0;
                double defensePoints = 0;

                attackPoints += 20;

                attackPoints += (attackerDroneUnit.Amount - 1) * 5;
                defensePoints += defenderDroneUnit!.Amount * 5;

                if (attackPoints > defensePoints)
                {
                    attackerDroneUnit.Amount -= (int)(request.SendDrone.NumberOfDrones * 0.2);

                    if (attackerDroneUnit.Amount < 0)
                    {
                        attackerDroneUnit.Amount = 0;
                    }

                    return attackerDroneUnit.EmpireId;
                }
                else
                {
                    defenderDroneUnit.Amount -= (int)(request.SendDrone.NumberOfDrones * 0.1);

                    if (defenderDroneUnit.Amount < 0)
                    {
                        defenderDroneUnit.Amount = 0;
                    }

                    return defenderDroneUnit.EmpireId;
                }
            }

            public static int? CalculateOpponentDefensivePoints(Domain.Models.EmpireModel.Base.Empire attackerEmpire,Guid? winnerId, ICollection<EmpireUnit> defenderUnits)
            {
                if (winnerId == attackerEmpire.Id)
                {
                    var defensePoints = 0;

                    foreach (var unit in defenderUnits.Where(d => d.Unit.Name != UnitEnum.ScoutDrone.GetDisplayName()))
                    {
                        var unitlevel = unit.Unit.UnitLevels.SingleOrDefault(ul => ul.Level == unit.Level && ul.UnitId == unit.UnitId);

                        if (unitlevel is null)
                        {
                            throw new Exception("Nincs ilyen szintje az egységnek.");
                        }

                        defensePoints += (int)(((unitlevel.DefensePoint + unit.FightPoint.DefensePointBonus) * unit.Amount) * unit.FightPoint.DefensePointMultiplier);
                    }

                    return defensePoints;
                }
                else
                {
                    return null;
                }
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SendDrone).SetValidator(new SendDroneValidator());
            }

            public class SendDroneValidator : AbstractValidator<SendDroneDto>
            {
                public SendDroneValidator()
                {
                    RuleFor(x => x.DronedEmpireId).NotNull().WithMessage("A kémkedett birodalom azonosítója nem lehet null.");
                    RuleFor(x => x.NumberOfDrones).NotNull().NotEmpty()
                        .GreaterThanOrEqualTo(1)
                        .WithMessage("Legalább 1 drónt kell küldened a kémkedéshez.");
                }
            }
        }
    }
}
