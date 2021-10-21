using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Alliance.Commands
{
    public static class LeaveAllianceCommand
    {
        public class Command : ICommand<bool>
        {
            public Guid? NewLeaderEmpireId { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IIdentityService identityService;

            public Handler(GalacticEmpireDbContext dbContext, IIdentityService identityService)
            {
                this.dbContext = dbContext;
                this.identityService = identityService;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Include(e => e.Alliance)
                        .ThenInclude(e => e.Alliance)
                            .ThenInclude(e => e.Members)
                    .Where(e => e.OwnerId == userId)
                    .FirstOrDefaultAsync();

                var allianceId = empire.Alliance.AllianceId;

                if (empire.Alliance.IsLeader)
                {
                    if (request.NewLeaderEmpireId == Guid.Empty)
                    {
                        throw new InvalidActionException("A szövetség vezéreként muszáj átadnod a vezetőséget valakinek.");
                    }

                    var member = await dbContext.AllianceMembers
                        .FirstOrDefaultAsync(e => e.EmpireId == empire.Id);

                    dbContext.AllianceMembers.Remove(member);

                    var newLeaderMember = await dbContext.AllianceMembers
                        .Include(e => e.Empire)
                        .FirstOrDefaultAsync(e => e.EmpireId == request.NewLeaderEmpireId);

                    if (newLeaderMember == null)
                    {
                        throw new NotFoundException("Nincsen ilyen tagja a szervezetnek.");
                    }

                    newLeaderMember.Empire.Alliance.IsLeader = true;
                    newLeaderMember.InvitationRight = true;
                }
                else
                {
                    var member = await dbContext.AllianceMembers
                        .FirstOrDefaultAsync(e => e.EmpireId == empire.Id);

                    dbContext.AllianceMembers.Remove(member);
                }

                await dbContext.SaveChangesAsync();

                return true;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {

            }
        }
    }
}
