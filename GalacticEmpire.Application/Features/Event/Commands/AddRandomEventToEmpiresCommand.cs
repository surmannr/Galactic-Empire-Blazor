using FluentValidation;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Shared.Dto.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Event.Commands
{
    public static class AddRandomEventToEmpiresCommand
    {
        public class Command : ICommand<MediatR.Unit>
        { 
            public RandomEventDto RandomEvent { get; set; }
        }

        public class Handler : IRequestHandler<Command, MediatR.Unit>
        {
            private readonly GalacticEmpireDbContext dbContext;

            public Handler(GalacticEmpireDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<MediatR.Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var random = new Random();

                var empires = await dbContext.Empires
                    .Include(e  => e.EmpireEvents)
                        .ThenInclude(e => e.Event)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.FightPoint)
                    .Include(e => e.EmpireMaterials)
                    .ToListAsync();

                var events = await dbContext.Events.ToListAsync();

                foreach (var empire in empires)
                {
                    var latestEmpireEvent = empire.EmpireEvents.LastOrDefault();

                    if (latestEmpireEvent != null)
                    {
                        latestEmpireEvent.Event.RemoveEffect(empire);
                    }

                    var randomNumber = random.Next(0, 100);
                    if (randomNumber <= request.RandomEvent.Seed)
                    {
                        var eventNumber = random.Next(0, events.Count);

                        var @event = events.ElementAt(eventNumber);

                        empire.EmpireEvents.Add(new EmpireEvent
                        {
                            EmpireId = empire.Id,
                            EventId = @event.Id,
                            Date = DateTimeOffset.Now
                        });

                        @event.ApplyEffect(empire);

                        await dbContext.SaveChangesAsync();
                    }
                }

                return new MediatR.Unit();
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.RandomEvent).SetValidator(new RandomEventValidator());
            }

            public class RandomEventValidator : AbstractValidator<RandomEventDto>
            {
                public RandomEventValidator()
                {
                    RuleFor(x => x.Seed).InclusiveBetween(1, 100).WithMessage("A seed adatnak 1 és 100 között kell lennie");
                }
            }
        }
    }
}
