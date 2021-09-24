using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.Empire
{
    public class EmpireMaterialEntityConfiguration : IEntityTypeConfiguration<Domain.Models.EmpireModel.EmpireMaterial>
    {
        public void Configure(EntityTypeBuilder<EmpireMaterial> builder)
        {
            builder.HasData(
                new EmpireMaterial()
                {
                    MaterialId = 1,
                    BaseProduction = BaseProductionConstants.BaseQuartzProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseQuartzAmount,
                    EmpireId = Guid.Parse("af378505-14cb-4f49-1111-ba2c8fdef77d")
                },
                new EmpireMaterial()
                {
                    MaterialId = 2,
                    BaseProduction = BaseProductionConstants.BaseFoodProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseFoodAmount,
                    EmpireId = Guid.Parse("af378505-14cb-4f49-1111-ba2c8fdef77d")
                },
                new EmpireMaterial()
                {
                    MaterialId = 3,
                    BaseProduction = BaseProductionConstants.BaseBitcoinProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseBitcoinAmount,
                    EmpireId = Guid.Parse("af378505-14cb-4f49-1111-ba2c8fdef77d")
                },

                new EmpireMaterial()
                {
                    MaterialId = 1,
                    BaseProduction = BaseProductionConstants.BaseQuartzProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseQuartzAmount,
                    EmpireId = Guid.Parse("72ff37e8-5888-47c6-1111-15844a6449b1")
                },
                new EmpireMaterial()
                {
                    MaterialId = 2,
                    BaseProduction = BaseProductionConstants.BaseFoodProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseFoodAmount,
                    EmpireId = Guid.Parse("72ff37e8-5888-47c6-1111-15844a6449b1")
                },
                new EmpireMaterial()
                {
                    MaterialId = 3,
                    BaseProduction = BaseProductionConstants.BaseBitcoinProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseBitcoinAmount,
                    EmpireId = Guid.Parse("72ff37e8-5888-47c6-1111-15844a6449b1")
                },

                new EmpireMaterial()
                {
                    MaterialId = 1,
                    BaseProduction = BaseProductionConstants.BaseQuartzProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseQuartzAmount,
                    EmpireId = Guid.Parse("a63a97aa-4ae8-4185-1111-be02286b1542")
                },
                new EmpireMaterial()
                {
                    MaterialId = 2,
                    BaseProduction = BaseProductionConstants.BaseFoodProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseFoodAmount,
                    EmpireId = Guid.Parse("a63a97aa-4ae8-4185-1111-be02286b1542")
                },
                new EmpireMaterial()
                {
                    MaterialId = 3,
                    BaseProduction = BaseProductionConstants.BaseBitcoinProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseBitcoinAmount,
                    EmpireId = Guid.Parse("a63a97aa-4ae8-4185-1111-be02286b1542")
                },

                new EmpireMaterial()
                {
                    MaterialId = 1,
                    BaseProduction = BaseProductionConstants.BaseQuartzProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseQuartzAmount,
                    EmpireId = Guid.Parse("c4393fff-8d3a-4508-1111-794916e9e997")
                },
                new EmpireMaterial()
                {
                    MaterialId = 2,
                    BaseProduction = BaseProductionConstants.BaseFoodProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseFoodAmount,
                    EmpireId = Guid.Parse("c4393fff-8d3a-4508-1111-794916e9e997")
                },
                new EmpireMaterial()
                {
                    MaterialId = 3,
                    BaseProduction = BaseProductionConstants.BaseBitcoinProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseBitcoinAmount,
                    EmpireId = Guid.Parse("c4393fff-8d3a-4508-1111-794916e9e997")
                },

                new EmpireMaterial()
                {
                    MaterialId = 1,
                    BaseProduction = BaseProductionConstants.BaseQuartzProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseQuartzAmount,
                    EmpireId = Guid.Parse("cbbd70fb-06cd-4368-1111-93c237980d8c")
                },
                new EmpireMaterial()
                {
                    MaterialId = 2,
                    BaseProduction = BaseProductionConstants.BaseFoodProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseFoodAmount,
                    EmpireId = Guid.Parse("cbbd70fb-06cd-4368-1111-93c237980d8c")
                },
                new EmpireMaterial()
                {
                    MaterialId = 3,
                    BaseProduction = BaseProductionConstants.BaseBitcoinProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseBitcoinAmount,
                    EmpireId = Guid.Parse("cbbd70fb-06cd-4368-1111-93c237980d8c")
                },

                new EmpireMaterial()
                {
                    MaterialId = 1,
                    BaseProduction = BaseProductionConstants.BaseQuartzProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseQuartzAmount,
                    EmpireId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6")
                },
                new EmpireMaterial()
                {
                    MaterialId = 2,
                    BaseProduction = BaseProductionConstants.BaseFoodProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseFoodAmount,
                    EmpireId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6")
                },
                new EmpireMaterial()
                {
                    MaterialId = 3,
                    BaseProduction = BaseProductionConstants.BaseBitcoinProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseBitcoinAmount,
                    EmpireId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6")
                },

                new EmpireMaterial()
                {
                    MaterialId = 1,
                    BaseProduction = BaseProductionConstants.BaseQuartzProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseQuartzAmount,
                    EmpireId = Guid.Parse("bf37d8cc-0744-4054-1111-603e6829799a")
                },
                new EmpireMaterial()
                {
                    MaterialId = 2,
                    BaseProduction = BaseProductionConstants.BaseFoodProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseFoodAmount,
                    EmpireId = Guid.Parse("bf37d8cc-0744-4054-1111-603e6829799a")
                },
                new EmpireMaterial()
                {
                    MaterialId = 3,
                    BaseProduction = BaseProductionConstants.BaseBitcoinProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseBitcoinAmount,
                    EmpireId = Guid.Parse("bf37d8cc-0744-4054-1111-603e6829799a")
                },

                new EmpireMaterial()
                {
                    MaterialId = 1,
                    BaseProduction = BaseProductionConstants.BaseQuartzProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseQuartzAmount,
                    EmpireId = Guid.Parse("488d40fe-e2c5-41e3-1111-dea16b7c2897")
                },
                new EmpireMaterial()
                {
                    MaterialId = 2,
                    BaseProduction = BaseProductionConstants.BaseFoodProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseFoodAmount,
                    EmpireId = Guid.Parse("488d40fe-e2c5-41e3-1111-dea16b7c2897")
                },
                new EmpireMaterial()
                {
                    MaterialId = 3,
                    BaseProduction = BaseProductionConstants.BaseBitcoinProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseBitcoinAmount,
                    EmpireId = Guid.Parse("488d40fe-e2c5-41e3-1111-dea16b7c2897")
                },

                new EmpireMaterial()
                {
                    MaterialId = 1,
                    BaseProduction = BaseProductionConstants.BaseQuartzProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseQuartzAmount,
                    EmpireId = Guid.Parse("0b62f843-4357-423b-1111-a2506ac91d5c")
                },
                new EmpireMaterial()
                {
                    MaterialId = 2,
                    BaseProduction = BaseProductionConstants.BaseFoodProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseFoodAmount,
                    EmpireId = Guid.Parse("0b62f843-4357-423b-1111-a2506ac91d5c")
                },
                new EmpireMaterial()
                {
                    MaterialId = 3,
                    BaseProduction = BaseProductionConstants.BaseBitcoinProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseBitcoinAmount,
                    EmpireId = Guid.Parse("0b62f843-4357-423b-1111-a2506ac91d5c")
                },

                new EmpireMaterial()
                {
                    MaterialId = 1,
                    BaseProduction = BaseProductionConstants.BaseQuartzProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseQuartzAmount,
                    EmpireId = Guid.Parse("c0b59d8d-58cc-4a54-a045-bf2a9341c658")
                },
                new EmpireMaterial()
                {
                    MaterialId = 2,
                    BaseProduction = BaseProductionConstants.BaseFoodProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseFoodAmount,
                    EmpireId = Guid.Parse("c0b59d8d-58cc-4a54-a045-bf2a9341c658")
                },
                new EmpireMaterial()
                {
                    MaterialId = 3,
                    BaseProduction = BaseProductionConstants.BaseBitcoinProduction,
                    ProductionMultiplier = 1,
                    Amount = BaseProductionConstants.BaseBitcoinAmount,
                    EmpireId = Guid.Parse("c0b59d8d-58cc-4a54-a045-bf2a9341c658")
                }
            );
        }
    }
}