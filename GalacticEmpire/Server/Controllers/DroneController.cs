using GalacticEmpire.Application.Features.Drone.Commands;
using GalacticEmpire.Application.Features.Drone.Queries;
using GalacticEmpire.Application.PaginationExtensions;
using GalacticEmpire.Shared.Dto.Drone;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticEmpire.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        private readonly IMediator mediator;

        public DroneController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("dronereport/details/{attackid}")]
        public async Task<DroneReportDetailsDto> GetAttackReportDetails(Guid attackid)
        {
            var query = new GetDroneReportDetailsQuery.Query
            {
                DroneAttackId = attackid
            };

            return await mediator.Send(query);
        }

        [HttpGet]
        [Route("dronereport/list")]
        public async Task<PagedResult<DroneReportPreviewDto>> GetAttackReportList([FromQuery] string filter, [FromQuery] PaginationData data)
        {
            var query = new GetDroneReportsPreviewQuery.Query
            {
                Filter = filter,
                PaginationData = data
            };

            return await mediator.Send(query);
        }

        [HttpPost]
        [Route("senddrone")]
        public async Task<bool> SendAttack([FromBody] SendDroneDto sendDrone)
        {
            var command = new SendDroneAttackCommand.Command
            {
                SendDrone = sendDrone
            };

            return await mediator.Send(command);
        }
    }
}
