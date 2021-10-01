using FluentValidation;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.User.Commands
{
    public static class CalculatePointsCommand
    {
        public class Command : ICommand<bool> { }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly GalacticEmpireDbContext dbContext;

            public Handler(GalacticEmpireDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var empires = await dbContext.Empires
                   .Include(e => e.EmpirePlanets)
                       .ThenInclude(e => e.EmpirePlanetUpgrades)
                   .Include(e => e.EmpireUnits)
                       .ThenInclude(e => e.Unit)
                   .Include(e => e.AttackerAttack)
                   .Include(e => e.DefenderAttack)
                   .Include(e => e.Owner)
                   .ToListAsync();

                foreach (var empire in empires)
                {
                    int populationPoints = empire.Population * PointConstants.Population;

                    int planetPoints = 0;
                    int upgradePoints = 0;

                    foreach (var planet in empire.EmpirePlanets)
                    {
                        planetPoints += PointConstants.Planets;
                        upgradePoints += planet.EmpirePlanetUpgrades.Count * PointConstants.Upgrades;
                    }

                    int militaryPoints = 0;
                    foreach (var unit in empire.EmpireUnits)
                    {
                        militaryPoints += unit.Unit.RankPoint * unit.Amount;
                    }

                    int battlePoints = 0;
                    foreach (var attack in empire.AttackerAttack)
                    {
                        if (attack.WinnerId == empire.Id)
                        {
                            battlePoints += PointConstants.WonBattles;
                        }
                        else if (attack.WinnerId == null)
                        {
                            battlePoints += PointConstants.DrawnBattles;
                        }
                        else
                        {
                            battlePoints += PointConstants.LostBattles;
                        }
                    }
                    foreach (var defender in empire.DefenderAttack)
                    {
                        if (defender.WinnerId == empire.Id)
                        {
                            battlePoints += PointConstants.WonBattles;
                        }
                        else if (defender.WinnerId == null)
                        {
                            battlePoints += PointConstants.DrawnBattles;
                        }
                        else
                        {
                            battlePoints += PointConstants.LostBattles;
                        }
                    }

                    empire.Owner.Points = populationPoints + planetPoints + upgradePoints + militaryPoints + battlePoints;
                }

                await dbContext.SaveChangesAsync();

                return true;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {

            }
        }
    }
}
