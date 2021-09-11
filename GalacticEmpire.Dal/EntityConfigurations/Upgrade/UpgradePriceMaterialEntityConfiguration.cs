using GalacticEmpire.Domain.Models.UpgradeModel;
using GalacticEmpire.Shared.Constants.Upgrade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.Upgrade
{
    public class UpgradePriceMaterialEntityConfiguration : IEntityTypeConfiguration<UpgradePriceMaterial>
    {
        public void Configure(EntityTypeBuilder<UpgradePriceMaterial> builder)
        {
            builder.HasData(
                new UpgradePriceMaterial()
                {
                    MaterialId = 1,
                    UpgradeId = 1,
                    Amount = UpgradePriceConstants.FuturisticResidentialArea_Quartz
                },
                new UpgradePriceMaterial()
                {
                    MaterialId = 1,
                    UpgradeId = 2,
                    Amount = UpgradePriceConstants.InterdimensionalGastrogarden_Quartz
                },
                new UpgradePriceMaterial()
                {
                    MaterialId = 1,
                    UpgradeId = 3,
                    Amount = UpgradePriceConstants.KineticShield_Quartz
                },
                new UpgradePriceMaterial()
                {
                    MaterialId = 1,
                    UpgradeId = 4,
                    Amount = UpgradePriceConstants.LaserWeapons_Quartz
                },
                new UpgradePriceMaterial()
                {
                    MaterialId = 1,
                    UpgradeId = 5,
                    Amount = UpgradePriceConstants.QuartzMine_Quartz
                },
                new UpgradePriceMaterial()
                {
                    MaterialId = 1,
                    UpgradeId = 6,
                    Amount = UpgradePriceConstants.SecretMilitaryBase_Quartz
                },
                new UpgradePriceMaterial()
                {
                    MaterialId = 1,
                    UpgradeId = 7,
                    Amount = UpgradePriceConstants.VibraniumArmor_Quartz
                },
                new UpgradePriceMaterial()
                {
                    MaterialId = 1,
                    UpgradeId = 8,
                    Amount = UpgradePriceConstants.VideocardExpansion_Quartz
                }
            );
        }
    }
}