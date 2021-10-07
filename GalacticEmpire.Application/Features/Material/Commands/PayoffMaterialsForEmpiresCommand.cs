using FluentValidation;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
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
    public static class PayoffMaterialsForEmpiresCommand
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
                   .ToListAsync();

                foreach (var empire in empires)
                {
                    foreach (var material in empire.EmpireMaterials)
                    {
                        material.Amount += (int)Math.Round(material.BaseProduction * material.ProductionMultiplier);
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
