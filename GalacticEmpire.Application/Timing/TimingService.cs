using GalacticEmpire.Application.Features.Event.Commands;
using GalacticEmpire.Application.Features.Material.Commands;
using GalacticEmpire.Application.Features.User.Commands;
using GalacticEmpire.Dal;
using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Shared.Constants;
using GalacticEmpire.Shared.Enums.Material;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Timing
{
    public class TimingService
    {
        private readonly IMediator mediator;

        public TimingService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task PayoffMaterials()
        {
            var command = new PayoffMaterialsForEmpiresCommand.Command();

            await mediator.Send(command);
        }

        public async Task PayoffEmpireMercenariesAndFeedEveryone()
        {
            var command = new PayoffEmpireMercenariesAndFeedEveryoneCommand.Command();

            await mediator.Send(command);
        }

        public async Task CalculatePoints()
        {
            var command = new CalculatePointsCommand.Command();

            await mediator.Send(command);
        }

        public async Task AddRandomEventToEmpires()
        {
            var command = new AddRandomEventToEmpiresCommand.Command
            {
                RandomEvent = new Shared.Dto.Event.RandomEventDto
                {
                    Seed = 50
                }
            };

            await mediator.Send(command);
        }
    }
}
