using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.User;
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
    public static class ChangePasswordCommand
    {
        public class Command : ICommand<bool>
        {
            public ChangePasswordDto ChangePassword { get; set; }
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

                var user = await dbContext.Users
                    .Include(c => c.Empire)
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                {
                    throw new NotFoundException("Nem létezik ilyen felhasználó.");
                }

                bool valid = await ValidatePassword(request.ChangePassword.NewPassword, userManager, user);
                if (valid)
                {
                    await userManager.AddPasswordAsync(user, request.ChangePassword.NewPassword);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                throw new InvalidActionException("Nem sikerült megváltoztatni a jelszavad. Kell betű, szám és speciális karakter.");
            }

            public async static Task<bool> ValidatePassword(string password, UserManager<Domain.Models.UserModel.Base.User> userManager, Domain.Models.UserModel.Base.User user)
            {
                var passwordValidator = new PasswordValidator<Domain.Models.UserModel.Base.User>();
                var valid = await passwordValidator.ValidateAsync(userManager, user, password);
                return (valid.Succeeded);
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ChangePassword).NotNull().SetValidator(new ChangePasswordValidator());
            }

            public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
            {
                public ChangePasswordValidator()
                {
                    RuleFor(x => x.NewPassword).NotNull().WithMessage("Az új jelszó kitöltése kötelező.");
                }
            }
        }
    }
}
