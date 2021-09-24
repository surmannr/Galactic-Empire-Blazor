using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Domain.Models.AllianceModel;
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
    public static class ManagaEmpireInvitationCommand
    {
        public class Command : ICommand<bool>
        {
            public Guid AllianceId { get; set; }
            public bool Accept { get; set; }
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
                    .Where(e => e.OwnerId == userId)
                    .FirstOrDefaultAsync();

                var empireInvitation = await dbContext.AllianceInvitations
                    .Where(e => e.AllianceId == request.AllianceId && e.InvitedEmpireId == empire.Id)
                    .FirstOrDefaultAsync();

                if (empireInvitation == null)
                {
                    throw new Exception("Nem létezik ilyen szövetségi meghívás.");
                }

                if (request.Accept)
                {
                    var allianceMember = new AllianceMember
                    {
                        AllianceId = request.AllianceId,
                        EmpireId = empire.Id,
                        InvitationRight = false,
                        IsLeader = false
                    };

                    dbContext.AllianceMembers.Add(allianceMember);
                }

                dbContext.AllianceInvitations.Remove(empireInvitation);

                await dbContext.SaveChangesAsync();

                return true;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AllianceId).NotNull().WithMessage("A szövetség azonosító nem lehet null.");
                RuleFor(x => x.Accept).NotNull().WithMessage("Az elfogadás nem lehet null.");
            }
        }
    }
}
