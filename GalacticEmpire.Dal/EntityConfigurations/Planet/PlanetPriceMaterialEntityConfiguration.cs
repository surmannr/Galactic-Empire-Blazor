using GalacticEmpire.Domain.Models.PlanetModel;
using GalacticEmpire.Shared.Constants.Planet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.Planet
{
    public class PlanetPriceMaterialEntityConfiguration : IEntityTypeConfiguration<PlanetPriceMaterial>
    {
        public void Configure(EntityTypeBuilder<PlanetPriceMaterial> builder)
        {
            builder.HasData(
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 1,
                    Amount = PlanetPriceConstants.Avypso_Bitcoin
                },
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 2,
                    Amount = PlanetPriceConstants.C137Earth_Bitcoin
                },
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 3,
                    Amount = PlanetPriceConstants.Cribatune_Bitcoin
                },
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 4,
                    Amount = PlanetPriceConstants.Darvis_Bitcoin
                },
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 5,
                    Amount = PlanetPriceConstants.Dillon_Bitcoin
                },
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 6,
                    Amount = PlanetPriceConstants.Gingeria_Bitcoin
                },
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 7,
                    Amount = PlanetPriceConstants.Heolara_Bitcoin
                },
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 8,
                    Amount = PlanetPriceConstants.Nusobos_Bitcoin
                },
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 9,
                    Amount = PlanetPriceConstants.Sidatania_Bitcoin
                },
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 10,
                    Amount = PlanetPriceConstants.Yoiphus_Bitcoin
                },
                new PlanetPriceMaterial()
                {
                    MaterialId = 3,
                    PlanetId = 11,
                    Amount = PlanetPriceConstants.Zuccars_Bitcoin
                }
            );
        }
    }
}
