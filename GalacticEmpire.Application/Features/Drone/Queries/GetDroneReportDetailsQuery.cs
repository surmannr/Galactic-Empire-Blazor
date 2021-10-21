using AutoMapper;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Drone;
using GalacticEmpire.Shared.Exceptions;
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
    public static class GetDroneReportDetailsQuery
    {
        public class Query : IRequest<DroneReportDetailsDto>
        {
            public Guid DroneAttackId { get; set; }
        }

        public class Handler : IRequestHandler<Query, DroneReportDetailsDto>
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

            public async Task<DroneReportDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Where(e => e.OwnerId == userId)
                    .FirstOrDefaultAsync();

                var droneAttack = await dbContext.DroneAttacks
                    .Include(d => d.Attacker)
                    .Include(d => d.Defender)
                    .Where(a => a.Id == request.DroneAttackId)
                    .Select(a => new DroneReportDetailsDto()
                    {
                        Id = a.Id,
                        Date = a.Date,
                        NumberOfAttackerDrones = a.NumberOfAttackerDrones,
                        NumberOfDefenderDrones = a.NumberOfDefenderDrones,
                        DefensePoints = a.DefenderDefensivePoints,
                        DronedEmpireName = a.Defender.Name,
                        OpponentEmpireName = a.Attacker.Name == empire.Name ? a.Defender.Name : a.Attacker.Name,
                        WinnerEmpireId = a.WinnerId,
                        WinnerEmpireName = GetWinnerEmpireName(a),
                        IsAttacker = a.AttackerId == empire.Id
                    })
                    .SingleOrDefaultAsync();

                if (droneAttack == null)
                {
                    throw new NotFoundException("Nem létezik ilyen azonosítójú kémkedés.");
                }

                return droneAttack;
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
                RuleFor(x => x.DroneAttackId).NotNull().WithMessage("A kémkedés azonosítója nem lehet null.");
            }
        }
    }
}
