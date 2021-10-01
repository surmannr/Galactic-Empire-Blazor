using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.ExtensionsAndServices.PaginationExtensions;
using GalacticEmpire.Application.PaginationExtensions;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Drone;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Drone.Queries
{
    public static class GetDroneReportsPreviewQuery
    {
        public class Query : IRequest<PagedResult<DroneReportPreviewDto>>
        {
            public PaginationData PaginationData { get; set; }
            public string? Filter { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedResult<DroneReportPreviewDto>>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IIdentityService identityService;

            public Handler(GalacticEmpireDbContext dbContext, IIdentityService identityService)
            {
                this.dbContext = dbContext;
                this.identityService = identityService;
            }

            public async Task<PagedResult<DroneReportPreviewDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Where(e => e.OwnerId == userId)
                    .FirstOrDefaultAsync();

                var droneAttacks = dbContext.DroneAttacks
                    .Include(a => a.Defender)
                    .Include(a => a.Attacker)
                    .Where(a => a.AttackerId == empire.Id || a.DefenderId == empire.Id)
                    .Select(a => new DroneReportPreviewDto()
                    {
                        Id = a.Id,
                        Date = a.Date,
                        OpponentEmpireName = a.Attacker.Name == empire.Name ? a.Defender.Name : a.Attacker.Name,
                        WinnerEmpireId = a.WinnerId,
                        WinnerEmpireName = GetWinnerEmpireName(a)
                    })
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.Filter) && !string.IsNullOrWhiteSpace(request.Filter))
                {
                    droneAttacks = droneAttacks
                        .Where(u => u.OpponentEmpireName.Contains(request.Filter) || u.WinnerEmpireName.Contains(request.Filter));
                }

                return droneAttacks.ToPagedList(request.PaginationData.PageSize, request.PaginationData.PageNumber);
            }

            public static string GetWinnerEmpireName(Domain.Models.AttackModel.DroneAttack attack)
            {
                if (attack.WinnerId == null)
                {
                    return string.Empty;
                }

                if (attack.Attacker.Id == attack.WinnerId)
                {
                    return attack.Attacker.Name;
                }
                else
                {
                    return attack.Defender.Name;
                }
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.PaginationData).SetValidator(new PaginationRuleValidator()).When(x => x.PaginationData is not null);
            }
        }
    }
}
