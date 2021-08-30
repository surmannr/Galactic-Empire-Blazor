using GalacticEmpire.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace GalacticEmpire.Dal.EntityConfigurations.Empire
{
    public class EmpireEntityConfiguration : IEntityTypeConfiguration<Domain.Models.EmpireModel.Base.Empire>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.EmpireModel.Base.Empire> builder)
        {
            builder.HasData(
                new Domain.Models.EmpireModel.Base.Empire()
                {
                    Id = Guid.Parse("af378505-14cb-4f49-1111-ba2c8fdef77d"),
                    Name = "Center",
                    OwnerId = "user1",
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits,
                },
                new Domain.Models.EmpireModel.Base.Empire()
                {
                    Id = Guid.Parse("72ff37e8-5888-47c6-1111-15844a6449b1"),
                    Name = "Melrose",
                    OwnerId = "user2",
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits,
                },
                new Domain.Models.EmpireModel.Base.Empire()
                {
                    Id = Guid.Parse("a63a97aa-4ae8-4185-1111-be02286b1542"),
                    Name = "Gale",
                    OwnerId = "user3",
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits,
                },
                new Domain.Models.EmpireModel.Base.Empire()
                {
                    Id = Guid.Parse("c4393fff-8d3a-4508-1111-794916e9e997"),
                    Name = "Algoma",
                    OwnerId = "user4",
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits,
                },
                new Domain.Models.EmpireModel.Base.Empire()
                {
                    Id = Guid.Parse("cbbd70fb-06cd-4368-1111-93c237980d8c"),
                    Name = "Carioca",
                    OwnerId = "user5",
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits,
                },
                new Domain.Models.EmpireModel.Base.Empire()
                {
                    Id = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6"),
                    Name = "Norway Maple",
                    OwnerId = "user6",
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits,
                },
                new Domain.Models.EmpireModel.Base.Empire()
                {
                    Id = Guid.Parse("bf37d8cc-0744-4054-1111-603e6829799a"),
                    Name = "Melody",
                    OwnerId = "user7",
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits,
                },
                new Domain.Models.EmpireModel.Base.Empire()
                {
                    Id = Guid.Parse("488d40fe-e2c5-41e3-1111-dea16b7c2897"),
                    Name = "Kipling",
                    OwnerId = "user8",
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits,
                },
                new Domain.Models.EmpireModel.Base.Empire()
                {
                    Id = Guid.Parse("0b62f843-4357-423b-1111-a2506ac91d5c"),
                    Name = "Londonderry",
                    OwnerId = "user9",
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits,
                },
                new Domain.Models.EmpireModel.Base.Empire()
                {
                    Id = Guid.Parse("c0b59d8d-58cc-4a54-a045-bf2a9341c658"),
                    Name = "Arkansas",
                    OwnerId = "user10",
                    Population = BaseProductionConstants.BasePopulation,
                    MaxNumberOfUnits = BaseProductionConstants.BaseMaxCountOfUnits,
                }
            );
        }
    }
}
