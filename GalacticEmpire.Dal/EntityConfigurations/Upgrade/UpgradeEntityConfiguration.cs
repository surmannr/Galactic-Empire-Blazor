using GalacticEmpire.Domain.Models.UpgradeModel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.Upgrade
{
    public class UpgradeEntityConfiguration : IEntityTypeConfiguration<Domain.Models.UpgradeModel.Base.Upgrade>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.UpgradeModel.Base.Upgrade> builder)
        {
            builder.HasData(
                new FuturisticResidentialAreaUpgrade()
                {
                    Id = 1
                },
                new InterdimensionalGastrogardenUpgrade()
                {
                    Id = 2
                },
                new KineticShieldUpgrade()
                {
                    Id = 3
                },
                new LaserWeaponsUpgrade()
                {
                    Id = 4
                },
                new QuartzMineUpgrade()
                {
                    Id = 5
                },
                new SecretMilitaryBaseUpgrade()
                {
                    Id = 6
                },
                new VibraniumArmorUpgrade()
                {
                    Id = 7
                },
                new VideocardExpansionUpgrade()
                {
                    Id = 8
                }
            );
        }
    }
}