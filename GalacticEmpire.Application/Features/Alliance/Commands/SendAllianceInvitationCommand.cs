using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Domain.Models.AllianceModel;
using GalacticEmpire.Shared.Dto.Alliance;
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
    public static class SendAllianceInvitationCommand
    {
        public class Command : ICommand<bool>
        {
            public SendAllianceInvitationDto SendAllianceInvitation { get; set; }
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
                    .Where(e => e.OwnerId == userId)
                    .FirstOrDefaultAsync();

                if (!empire.Alliance.InvitationRight)
                {
                    throw new Exception("Nincsen jogod az új birodalmak felvételére a szövetségbe.");
                }

                if (empire.Id == request.SendAllianceInvitation.InvitedEmpireId)
                {
                    throw new Exception("Nem hívhatod meg saját magadat a szövetségbe.");
                }

                var is_member = await dbContext.AllianceMembers
                    .Where(e => e.AllianceId == empire.Alliance.AllianceId && e.EmpireId == request.SendAllianceInvitation.InvitedEmpireId)
                    .AnyAsync();

                if (is_member)
                {
                    throw new Exception("A meghívott birodalom már tagja a szövetségnek.");
                }

                var is_invited = await dbContext.AllianceInvitations
                    .Where(e => e.AllianceId == empire.Alliance.AllianceId && e.InvitedEmpireId == request.SendAllianceInvitation.InvitedEmpireId)
                    .AnyAsync();

                if (is_invited)
                {
                    throw new Exception("A birodalom már meghívásra került a szövetségbe.");
                }

                var invitation = new AllianceInvitation
                {
                    AllianceId = empire.Alliance.AllianceId,
                    Date = DateTimeOffset.Now,
                    InvitedEmpireId = request.SendAllianceInvitation.InvitedEmpireId,
                    InviterEmpireId = empire.Id
                };

                dbContext.AllianceInvitations.Add(invitation);

                await dbContext.SaveChangesAsync();

                return true;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SendAllianceInvitation)
                    .NotNull()
                    .WithMessage("A meghívott birodalmat kötelező kitölteni.")
                    .SetValidator(new SendAllianceInvitationValidator());
            }

            public class SendAllianceInvitationValidator : AbstractValidator<SendAllianceInvitationDto>
            {
                public SendAllianceInvitationValidator()
                {
                    RuleFor(x => x.InvitedEmpireId).NotNull().WithMessage("A szövetség azonosító nem maradhat üresen.");
                }
            }
        }
    }
}
