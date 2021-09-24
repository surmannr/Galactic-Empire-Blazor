using GalacticEmpire.Domain.Models.UnitModel;
using GalacticEmpire.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.Unit
{
    public class UnitPriceMaterialEntityConfiguration : IEntityTypeConfiguration<Domain.Models.UnitModel.UnitPriceMaterial>
    {
        public void Configure(EntityTypeBuilder<UnitPriceMaterial> builder)
        {
            builder.HasData(
                new UnitPriceMaterial
                {
                    UnitId = 1,
                    MaterialId = 3,
                    Amount = UnitsConstants.SolarSail_Price
                },
                new UnitPriceMaterial
                {
                    UnitId = 2,
                    MaterialId = 3,
                    Amount = UnitsConstants.SpaceCruiser_Price
                },
                new UnitPriceMaterial
                {
                    UnitId = 3,
                    MaterialId = 3,
                    Amount = UnitsConstants.IronMan_Price
                },
                new UnitPriceMaterial
                {
                    UnitId = 4,
                    MaterialId = 3,
                    Amount = UnitsConstants.MilleniumFalcon_Price
                },
                new UnitPriceMaterial
                {
                    UnitId = 5,
                    MaterialId = 3,
                    Amount = UnitsConstants.ScoutDrone_Price
                }
            );
        }
    }
}