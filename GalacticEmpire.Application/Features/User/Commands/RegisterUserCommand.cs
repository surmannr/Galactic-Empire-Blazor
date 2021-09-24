using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.UserModel.Base;
using GalacticEmpire.Shared.Constants;
using GalacticEmpire.Shared.Constants.Role;
using GalacticEmpire.Shared.Dto.User;
using GalacticEmpire.Shared.Enums.Material;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.User.Commands
{
    public static class RegisterUserCommand
    {
        public class Command : ICommand<bool>
        {
            public RegisterDto RegisterDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly UserManager<Domain.Models.UserModel.Base.User> userManager;

            public Handler(GalacticEmpireDbContext dbContext, UserManager<Domain.Models.UserModel.Base.User> userManager)
            {
                this.dbContext = dbContext;
                this.userManager = userManager;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new Domain.Models.UserModel.Base.User() { 
                    UserName = request.RegisterDto.UserName,
                    Email = request.RegisterDto.Email
                };

                var result = await userManager.CreateAsync(user, request.RegisterDto.Password);
                await userManager.AddToRoleAsync(user, Roles.User);

                var materials = await dbContext.Materials.ToListAsync();
                var units = await dbContext.Units.Include(u => u.UnitLevels).ToListAsync();

                var empireModel = new Domain.Models.EmpireModel.Base.Empire
                {
                    Id = Guid.NewGuid(),
                    Name = request.RegisterDto.EmpireName,
                    OwnerId = user.Id,
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfPopulation = BaseProductionConstants.BaseMaxCountOfPopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits
                };

                var empireResult = dbContext.Empires.Add(empireModel);

                var empire = empireResult.Entity;

                foreach(var material in materials)
                {
                    var matprod = GetMaterialProduction(material);

                    var empMaterial = new EmpireMaterial()
                    {
                        MaterialId = material.Id,
                        ProductionMultiplier = 1,
                        BaseProduction = matprod.BaseProduction,
                        Amount = matprod.Amount,
                        EmpireId = empire.Id
                    };

                    dbContext.EmpireMaterials.Add(empMaterial);
                }

                await dbContext.SaveChangesAsync();

                foreach(var unit  in units)
                {
                    for(int i = 1; i <= unit.UnitLevels.Max(u => u.Level); i++)
                    {
                        var empUnits = new EmpireUnit()
                        {
                            EmpireId = empire.Id,
                            Amount = 0,
                            Level = i,
                            UnitId = unit.Id,
                            FightPoint = new FightPoint()
                            {
                                AttackPointMultiplier = 1,
                                DefensePointMultiplier = 1,
                                AttackPointBonus = 0,
                                DefensePointBonus = 0,
                            }
                        };

                        dbContext.EmpireUnits.Add(empUnits);
                    }
                }

                await dbContext.SaveChangesAsync();

                return result.Succeeded;
            }

            private MaterialProduction GetMaterialProduction(Domain.Models.MaterialModel.Base.Material material)
            {
                MaterialProduction materialProduction = new MaterialProduction();
                MaterialEnum? materialEnum = new MaterialEnum();

                if (material.Name == MaterialEnum.Bitcoin.GetDisplayName())
                    materialEnum = MaterialEnum.Bitcoin;
                else if (material.Name == MaterialEnum.Quartz.GetDisplayName())
                    materialEnum = MaterialEnum.Quartz;
                else if (material.Name == MaterialEnum.Food.GetDisplayName())
                    materialEnum = MaterialEnum.Food;
                else materialEnum = null;

                switch (materialEnum)
                {
                    case MaterialEnum.Bitcoin:
                        materialProduction.BaseProduction = BaseProductionConstants.BaseBitcoinProduction;
                        materialProduction.Amount = BaseProductionConstants.BaseBitcoinAmount;
                        break;
                    case MaterialEnum.Quartz:
                        materialProduction.BaseProduction = BaseProductionConstants.BaseQuartzProduction;
                        materialProduction.Amount = BaseProductionConstants.BaseQuartzAmount;
                        break;
                    case MaterialEnum.Food:
                        materialProduction.BaseProduction = BaseProductionConstants.BaseFoodProduction;
                        materialProduction.Amount = BaseProductionConstants.BaseFoodAmount;
                        break;
                    default:
                        materialProduction.BaseProduction = 100;
                        materialProduction.Amount = 10000;
                        break;
                }

                return materialProduction;
            }

            public class MaterialProduction
            {
                public int Amount { get; set; }
                public int BaseProduction { get; set; }
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.RegisterDto).NotNull().SetValidator(new RegisterDtoValidator()).WithMessage("Nem lehet üres a regisztráló modell.");
            }

            public class RegisterDtoValidator : AbstractValidator<RegisterDto>
            {
                public RegisterDtoValidator()
                {
                    RuleFor(x => x.Email)
                        .NotNull()
                        .NotEmpty()
                        .EmailAddress()
                        .WithMessage("Az email cím nem maradhat üresen és meg kell feleljen az email cím formátumnak.");

                    RuleFor(x => x.Password)
                        .NotNull()
                        .NotEmpty()
                        .Must((user, cancellation) => PasswordMatchRegex(user.Password))
                        .WithMessage("A jelszó legalább 6 karakterből kell álljon, valamint tartalmaznia kell legalább egy: kisbetűt, nagybetűt, számot, speciális karaktert (._#$^+=!?*()@%&)");

                    RuleFor(user => user.Password)
                        .NotNull()
                        .NotEmpty()
                        .Equal(u => u.ConfirmPassword)
                        .WithMessage("A megadott jelszavaknak egyezni kell.");

                    RuleFor(user => user.ConfirmPassword)
                        .NotNull()
                        .WithMessage("A jelszó megerősítése mező nem maradhat üresen.");

                    RuleFor(user => user.EmpireName)
                        .NotNull()
                        .NotEmpty()
                        .WithMessage("Az birodalom nevének megadása kötelező.");
                }

                private bool PasswordMatchRegex(string password)
                {
                    var pwRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[._#$^+=!*?()@%&]).{6,}$");

                    return pwRegex.IsMatch(password);
                }
            }
        }
    }
}
