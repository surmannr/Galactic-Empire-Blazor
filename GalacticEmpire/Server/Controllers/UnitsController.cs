using GalacticEmpire.Application.Features.Unit.Commands;
using GalacticEmpire.Application.Features.Unit.Queries;
using GalacticEmpire.Shared.Dto.Unit;
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
    public class UnitsController : ControllerBase
    {
        private readonly IMediator mediator;

        public UnitsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<UnitDto>> GetAllUnits([FromQuery] GetAllUnitsQuery.Query query)
        {
            return await mediator.Send(query);
        }

        [HttpPost]
        [Route("buy-units")]
        public async Task<bool> BuyUnits([FromBody] BuyUnitsCollectionDto buyUnitsCollection)
        {
            var command = new BuyUnitsCommand.Command
            {
                UnitsCollection = buyUnitsCollection
            };

            return await mediator.Send(command);
        }
    }
}
