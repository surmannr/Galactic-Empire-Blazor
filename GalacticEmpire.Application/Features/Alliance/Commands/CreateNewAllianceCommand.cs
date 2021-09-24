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
    public static class CreateNewAllianceCommand
    {
        public class Command : ICommand<bool>
        {
            public string AllianceName { get; set; }
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

                var newAlliance = new Domain.Models.AllianceModel.Base.Alliance()
                {
                    Name = request.AllianceName,
                    Members = new List<AllianceMember>()
                    {
                        new AllianceMember()
                        {
                            EmpireId = empire.Id,
                            InvitationRight = true,
                            IsLeader = true
                        }
                    }
                };

                var entity = dbContext.Alliances.Add(newAlliance);

                await dbContext.SaveChangesAsync();

                return true;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AllianceName).NotNull().NotEmpty().WithMessage("Az új birodalmi szövetség neve nem lehet üres.");
            }
        }
    }
}
