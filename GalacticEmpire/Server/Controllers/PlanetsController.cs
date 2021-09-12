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
    }
}
