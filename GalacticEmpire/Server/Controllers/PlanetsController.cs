using GalacticEmpire.Application.Features.Planet.Commands;
using GalacticEmpire.Application.Features.Planet.Queries;
using GalacticEmpire.Shared.Dto.Planet;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticEmpire.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PlanetsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<PlanetDto>> GetAllPlanets([FromQuery] GetAllPlanetsQuery.Query query)
        {
            return await mediator.Send(query);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<PlanetDetailsDto> GetAllPlanets(int id)
        {
            var query = new GetPlanetDetailsQuery.Query()
            {
                Id = id
            };

            return await mediator.Send(query);
        }

        [HttpGet]
        [Route("capturable-all")]
        public async Task<List<PlanetDetailsDto>> GetAllCapturablePlanets([FromQuery] GetAllCapturablePlanetsQuery.Query query)
        {
            return await mediator.Send(query);
        }

        [HttpPost]
        [Route("buy-planet/{planetid}")]
        public async Task<bool> BuyPlanet(int planetid, [FromQuery] string connectionId)
        {
            var command = new BuyPlanetCommand.Command
            {
                BuyPlanet = new BuyPlanetDto()
                {
                    PlanetId = planetid
                },
                ConnectionId = connectionId
            };

            return await mediator.Send(command);
        }
    }
}
