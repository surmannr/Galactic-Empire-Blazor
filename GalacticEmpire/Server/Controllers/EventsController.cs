using GalacticEmpire.Application.Features.Event.Commands;
using GalacticEmpire.Application.Features.Event.Queries;
using GalacticEmpire.Application.PaginationExtensions;
using GalacticEmpire.Shared.Dto.Event;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalacticEmpire.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator mediator;

        public EventsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<EventDto>> GetAllEvents([FromQuery] GetAllEventsQuery.Query query)
        {
            return await mediator.Send(query);
        }

        [HttpGet]
        [Authorize]
        [Route("user-events")]
        public async Task<PagedResult<EventDto>> GetAllUserEventsPaged([FromQuery] PaginationData data, [FromQuery] string filter)
        {
            var query = new GetAllUserEventsPagedQuery.Query()
            {
                Filter = filter,
                PaginationData = data
            };

            return await mediator.Send(query);
        }

        [HttpPost]
        [Route("add-random-events")]
        public async Task AddRandomEvents([FromQuery] RandomEventDto randomEvent)
        {
            var command = new AddRandomEventToEmpiresCommand.Command()
            {
                RandomEvent = randomEvent
            };

            await mediator.Send(command);
        }
    }
}