using GalacticEmpire.Application.Features.Event.Queries;
using GalacticEmpire.Shared.Dto.Event;
using MediatR;
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
        public async Task<List<EventDto>> GetEvents([FromQuery] GetAllEventsQuery.Query query)
        {
            return await mediator.Send(query);
        }
    }
}