using GalacticEmpire.Application.Features.User.Commands;
using GalacticEmpire.Application.Features.User.Queries;
using GalacticEmpire.Application.PaginationExtensions;
using GalacticEmpire.Shared.Dto.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("ranklist")]
        public async Task<PagedResult<UserRankDto>> GetRankList([FromQuery] string filter, [FromQuery] PaginationData data)
        {
            var query = new GetRankListQuery.Query
            {
                Filter = filter,
                PaginationData = data
            };

            return await mediator.Send(query);
        }

        [HttpPost]
        [Route("register")]
        public async Task<bool> Register([FromBody] RegisterUserCommand.Command command)
        {
            var result = await mediator.Send(command);
            return result.Succeeded;
        }

        [HttpGet]
        [Authorize]
        [Route("attackable-users")]
        public async Task<List<AttackableUserDto>> GetAttackableUsers([FromQuery] string filter)
        {
            var query = new GetAttackableUsersQuery.Query
            {
                Filter = filter
            };

            return await mediator.Send(query);
        }
    }
}
