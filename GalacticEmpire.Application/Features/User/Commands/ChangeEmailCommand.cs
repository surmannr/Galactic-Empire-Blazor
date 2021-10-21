using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.User.Commands
{
    public static class ChangeEmailCommand
    {
        public class Command : ICommand<bool>
        {
            public string NewEmail { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly UserManager<Domain.Models.UserModel.Base.User> userManager;
            private readonly IIdentityService identityService;
            private readonly GalacticEmpireDbContext dbContext;

            public Handler(UserManager<Domain.Models.UserModel.Base.User> userManager, IIdentityService identityService, GalacticEmpireDbContext dbContext)
            {
                this.userManager = userManager;
                this.identityService = identityService;
                this.dbContext = dbContext;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                {
                    throw new NotFoundException("Nem létezik ilyen felhasználó.");
                }

                user.Email = request.NewEmail;
                await dbContext.SaveChangesAsync();

                return true;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.NewEmail).NotNull().WithMessage("Az új email cím kitöltése kötelező.");
            }
        }
    }
}
