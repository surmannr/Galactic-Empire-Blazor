using GalacticEmpire.Application.Features.Attack.Commands;
using GalacticEmpire.Application.Features.Attack.Queries;
using GalacticEmpire.Application.PaginationExtensions;
using GalacticEmpire.Shared.Dto.Attack;
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
    public class AttackController : ControllerBase
    {
        private readonly IMediator mediator;

        public AttackController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("attackreport/details/{attackid}")]
        public async Task<AttackReportDetailsDto> GetAttackReportDetails(Guid attackid)
        {
            var query = new GetAttackReportDetailsQuery.Query
            {
                AttackId = attackid
            };

            return await mediator.Send(query);
        }

        [HttpGet]
        [Route("attackreport/list")]
        public async Task<PagedResult<AttackReportPreviewDto>> GetAttackReportList([FromQuery] string filter, [FromQuery] PaginationData data)
        {
            var query = new GetAttackReportsPreviewQuery.Query
            {
                Filter = filter,
                PaginationData = data
            };

            return await mediator.Send(query);
        }

        [HttpPost]
        [Route("sendattack")]
        public async Task<bool> SendAttack([FromBody] SendAttackDto sendAttack, [FromQuery] string connectionId)
        {
            var command = new SendAttackCommand.Command
            {
                SendAttackDto = sendAttack,
                ConnectionId = connectionId
            };

            return await mediator.Send(command);
        }
    }
}
