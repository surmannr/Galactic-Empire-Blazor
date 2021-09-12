using GalacticEmpire.Application.Features.Empire.Queries;
using GalacticEmpire.Shared.Dto.Empire;
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
    public class EmpiresController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmpiresController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("details")]
        public async Task<EmpireDetailsDto> GetEmpireDetails([FromQuery] GetEmpireDetailsQuery.Query query)
        {
            return await mediator.Send(query);
        }
    }
}
