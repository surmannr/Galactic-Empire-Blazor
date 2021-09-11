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
    public class PlanetPropertyEntityConfiguration : IEntityTypeConfiguration<PlanetProperty>
    {
        public void Configure(EntityTypeBuilder<PlanetProperty> builder)
        {
            builder.HasData(
                new PlanetProperty
                {
                    PlanetId = 1,
                    BaseFood = PlanetEffectConstants.Avypso_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Avypso_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Avypso_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Avypso_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.Avypso_MaxUnitCount
                },
                new PlanetProperty
                {
                    PlanetId = 2,
                    BaseFood = PlanetEffectConstants.C137Earth_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.C137Earth_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.C137Earth_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.C137Earth_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.C137Earth_MaxUnitCount
                },
                new PlanetProperty
                {
                    PlanetId = 3,
                    BaseFood = PlanetEffectConstants.Cribatune_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Cribatune_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Cribatune_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Cribatune_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.Cribatune_MaxUnitCount
                },
                new PlanetProperty
                {
                    PlanetId = 4,
                    BaseFood = PlanetEffectConstants.Darvis_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Darvis_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Darvis_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Darvis_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.Darvis_MaxUnitCount
                },
                new PlanetProperty
                {
                    PlanetId = 5,
                    BaseFood = PlanetEffectConstants.Dillon_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Dillon_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Dillon_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Dillon_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.Dillon_MaxUnitCount
                },
                new PlanetProperty
                {
                    PlanetId = 6,
                    BaseFood = PlanetEffectConstants.Gingeria_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Gingeria_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Gingeria_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Gingeria_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.Gingeria_MaxUnitCount
                },
                new PlanetProperty
                {
                    PlanetId = 7,
                    BaseFood = PlanetEffectConstants.Heolara_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Heolara_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Heolara_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Heolara_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.Heolara_MaxUnitCount
                },
                new PlanetProperty
                {
                    PlanetId = 8,
                    BaseFood = PlanetEffectConstants.Nusobos_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Nusobos_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Nusobos_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Nusobos_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.Nusobos_MaxUnitCount
                },
                new PlanetProperty
                {
                    PlanetId = 9,
                    BaseFood = PlanetEffectConstants.Sidatania_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Sidatania_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Sidatania_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Sidatania_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.Sidatania_MaxUnitCount
                },
                new PlanetProperty
                {
                    PlanetId = 10,
                    BaseFood = PlanetEffectConstants.Yoiphus_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Yoiphus_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Yoiphus_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Yoiphus_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.Yoiphus_MaxUnitCount
                },
                new PlanetProperty
                {
                    PlanetId = 11,
                    BaseFood = PlanetEffectConstants.Zuccars_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Zuccars_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Zuccars_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Zuccars_MaxPopulationCount,
                    MaxUnitCount = PlanetEffectConstants.Zuccars_MaxUnitCount
                }
            );
        }
    }
}