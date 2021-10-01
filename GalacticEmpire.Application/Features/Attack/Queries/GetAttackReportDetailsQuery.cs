using AutoMapper;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Attack;
using GalacticEmpire.Shared.Dto.Unit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Attack.Queries
{
    public static class GetAttackReportDetailsQuery
    {
        public class Query : IRequest<AttackReportDetailsDto>
        {
            public Guid AttackId { get; set; }
        }

        public class Handler : IRequestHandler<Query, AttackReportDetailsDto>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IIdentityService identityService;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IIdentityService identityService, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.identityService = identityService;
                this.mapper = mapper;
            }

            public async Task<AttackReportDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Where(e => e.OwnerId == userId)
                    .FirstOrDefaultAsync();

                var attack = await dbContext.Attacks
                    .Include(a => a.DefenseUnits)
                        .ThenInclude(a => a.Unit)
                    .Include(a => a.Defender)
                    .Include(a => a.Attacker)
                    .Include(a => a.AttackUnits)
                        .ThenInclude(a => a.Unit)
                    .Where(a => a.Id == request.AttackId)
                    .Select(a => new AttackReportDetailsDto()
                    {
                        Id = a.Id,
                        Date = a.Date,
                        AttackUnits = mapper.Map<List<BattleUnitDto>>(a.AttackUnits),
                        DefenseUnits = mapper.Map<List<BattleUnitDto>>(a.DefenseUnits),
                        OpponentEmpireName = a.Attacker.Name == empire.Name ? a.Defender.Name : a.Attacker.Name,
                        WinnerEmpireId = a.WinnerId,
                        WinnerEmpireName =  GetWinnerEmpireName(a)
                    })
                    .SingleOrDefaultAsync();

                if (attack == null)
                {
                    throw new Exception("Nem létezik ilyen azonosítójú támadás.");
                }

                return attack;
            }

            public static string GetWinnerEmpireName(Domain.Models.AttackModel.Base.Attack attack)
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
                RuleFor(x => x.AttackId).NotNull().WithMessage("A támadás azonosítója nem lehet null");
            }
        }
    }
}
