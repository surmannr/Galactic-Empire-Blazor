using FluentValidation;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Enums.Material;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Material.Commands
{
    public static class PayoffEmpireMercenariesAndFeedEveryoneCommand
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
                  .Include(e => e.EmpireMaterials)
                      .ThenInclude(e => e.Material)
                  .Include(e => e.EmpireUnits)
                      .ThenInclude(e => e.Unit)
                        .ThenInclude(e => e.UnitPriceMaterials)
                  .ToListAsync();

                foreach (var empire in empires)
                {
                    foreach (var unit in empire.EmpireUnits.OrderByDescending(c => c.Unit.UnitPriceMaterials.Sum(c => c.Amount)))
                    {
                        int requiredFood = unit.Unit.SupplyPerHour * unit.Amount;
                        int requiredMercenary = unit.Unit.MercenaryPerHour * unit.Amount;

                        var foodUnitMaterial = empire.EmpireMaterials.SingleOrDefault(cm => cm.Material.Name == MaterialEnum.Food.GetDisplayName());
                        var mercenaryUnitMaterial = empire.EmpireMaterials.SingleOrDefault(cm => cm.Material.Name == MaterialEnum.Quartz.GetDisplayName());

                        if(foodUnitMaterial is null || mercenaryUnitMaterial is null)
                        {
                            throw new Exception("Nincsen ilyen nyersanyagod.");
                        }

                        if (requiredFood <= foodUnitMaterial.Amount && requiredMercenary <= mercenaryUnitMaterial.Amount)
                        {
                            foodUnitMaterial.Amount -= requiredFood;
                            mercenaryUnitMaterial.Amount -= requiredMercenary;
                        }
                        else
                        {
                            int numberOfAlive = 0;

                            for (int i = 0; i <= unit.Amount; i++)
                            {
                                int food = unit.Unit.SupplyPerHour * i;
                                int mercenary = unit.Unit.MercenaryPerHour * i;

                                if (food <= foodUnitMaterial.Amount && mercenary <= mercenaryUnitMaterial.Amount)
                                {
                                    numberOfAlive = i;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            unit.Amount = numberOfAlive;

                            foodUnitMaterial.Amount -= numberOfAlive * unit.Unit.SupplyPerHour;
                            mercenaryUnitMaterial.Amount -= numberOfAlive * unit.Unit.MercenaryPerHour;
                        }
                    }

                    empire.Population += 10000;

                    if(empire.Population >= empire.MaxNumberOfPopulation)
                    {
                        empire.Population = empire.MaxNumberOfPopulation;
                    }
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
