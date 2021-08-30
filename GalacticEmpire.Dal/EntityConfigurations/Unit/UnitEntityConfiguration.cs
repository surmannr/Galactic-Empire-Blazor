using GalacticEmpire.Shared.Constants;
using GalacticEmpire.Shared.Enums.Unit;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.Unit
{
    public class UnitEntityConfiguration : IEntityTypeConfiguration<Domain.Models.UnitModel.Base.Unit>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.UnitModel.Base.Unit> builder)
        {
            builder.HasData(
                new Domain.Models.UnitModel.Base.Unit()
                {
                    Id = 1,
                    Name = UnitEnum.SolarSail.GetDisplayName(),
                    MercenaryPerHour = UnitsConstants.SolarSail_MercenaryPerHour,
                    SupplyPerHour = UnitsConstants.SolarSail_SupplyPerHour,
                    RankPoint = UnitsConstants.SolarSail_RankPoint,
                    ImageUrl = @"later"
                },
                new Domain.Models.UnitModel.Base.Unit()
                {
                    Id = 2,
                    Name = UnitEnum.SpaceCruiser.GetDisplayName(),
                    MercenaryPerHour = UnitsConstants.SpaceCruiser_MercenaryPerHour,
                    SupplyPerHour = UnitsConstants.SpaceCruiser_SupplyPerHour,
                    RankPoint = UnitsConstants.SpaceCruiser_RankPoint,
                    ImageUrl = @"later"
                },
                new Domain.Models.UnitModel.Base.Unit()
                {
                    Id = 3,
                    Name = UnitEnum.IronMan.GetDisplayName(),
                    MercenaryPerHour = UnitsConstants.IronMan_MercenaryPerHour,
                    SupplyPerHour = UnitsConstants.IronMan_SupplyPerHour,
                    RankPoint = UnitsConstants.IronMan_RankPoint,
                    ImageUrl = @"later"
                },
                new Domain.Models.UnitModel.Base.Unit()
                {
                    Id = 4,
                    Name = UnitEnum.MilleniumFalcon.GetDisplayName(),
                    MercenaryPerHour = UnitsConstants.MilleniumFalcon_MercenaryPerHour,
                    SupplyPerHour = UnitsConstants.MilleniumFalcon_SupplyPerHour,
                    RankPoint = UnitsConstants.MilleniumFalcon_RankPoint,
                    ImageUrl = @"later"
                },
                new Domain.Models.UnitModel.Base.Unit()
                {
                    Id = 5,
                    Name = UnitEnum.ScoutDrone.GetDisplayName(),
                    MercenaryPerHour = UnitsConstants.ScoutDrone_MercenaryPerHour,
                    SupplyPerHour = UnitsConstants.ScoutDrone_SupplyPerHour,
                    RankPoint = UnitsConstants.ScoutDrone_RankPoint,
                    ImageUrl = @"later"
                }
            );
        }
    }
}
