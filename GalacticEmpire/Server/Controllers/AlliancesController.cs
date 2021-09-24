using GalacticEmpire.Application.Features.Alliance.Commands;
using GalacticEmpire.Application.Features.Alliance.Queries;
using GalacticEmpire.Application.PaginationExtensions;
using GalacticEmpire.Shared.Dto.Alliance;
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
    public class AlliancesController : ControllerBase
    {
        private readonly IMediator mediator;

        public AlliancesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("create-alliance")]
        public async Task<bool> CreateNewAlliance([FromQuery] CreateNewAllianceCommand.Command command)
        {
            return await mediator.Send(command);
        }

        [HttpPost]
        [Route("invite-empire")]
        public async Task<bool> InviteNewMember([FromBody] SendAllianceInvitationDto sendAllianceInvitation)
        {
            var command = new SendAllianceInvitationCommand.Command
            {
                SendAllianceInvitation = sendAllianceInvitation
            };

            return await mediator.Send(command);
        }

        [HttpGet]
        [Route("empire-invitations")]
        public async Task<PagedResult<AllianceInvitationDto>> GetAllEmpireInvitations([FromQuery] string filter, [FromQuery] PaginationData data)
        {
            var query = new GetEmpireAllianceInvitationsQuery.Query
            {
                Filter = filter,
                PaginationData = data
            };

            return await mediator.Send(query);
        }

        [HttpPost]
        [Route("manage-invitation/{allianceId}")]
        public async Task<bool> ManageAllianceInvitation(Guid allianceId, [FromQuery] bool accept)
        {
            var command = new ManagaEmpireInvitationCommand.Command
            {
                Accept = accept,
                AllianceId = allianceId
            };

            return await mediator.Send(command);
        }

        [HttpGet]
        [Route("details/{allianceId}")]
        public async Task<AllianceDetailsDto> GetAllianceDetails(Guid allianceId)
        {
            var query = new GetAllianceDetailsQuery.Query
            {
                AllianceId = allianceId
            };

            return await mediator.Send(query);
        }

        [HttpGet]
        [Route("all")]
        public async Task<PagedResult<AlliancePreviewDto>> GetAllAlliances([FromQuery] string filter, [FromQuery] PaginationData data)
        {
            var query = new GetAllAlliancesQuery.Query
            {
                Filter = filter,
                PaginationData = data
            };

            return await mediator.Send(query);
        }

        [HttpDelete]
        [Route("leave-alliance")]
        public async Task<bool> LeaveAlliance([FromQuery] Guid newLeaderEmpireId)
        {
            var command = new LeaveAllianceCommand.Command
            {
                NewLeaderEmpireId = newLeaderEmpireId
            };

            return await mediator.Send(command);
        }
    }
}
