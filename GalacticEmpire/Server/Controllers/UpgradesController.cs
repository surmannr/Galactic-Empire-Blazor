using GalacticEmpire.Application.Features.Upgrade.Queries;
using GalacticEmpire.Shared.Dto.Upgrade;
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
    public class UpgradesController : ControllerBase
    {
        private readonly IMediator mediator;

        public UpgradesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<UpgradeDto>> GetAllUpgrades([FromQuery] GetAllUpgradesQuery.Query query)
        {
            return await mediator.Send(query);
        }
    }
}
