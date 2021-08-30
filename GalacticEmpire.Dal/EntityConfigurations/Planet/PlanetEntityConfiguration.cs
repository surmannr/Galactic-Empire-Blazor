using GalacticEmpire.Domain.Models.PlanetModel.Type;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.Planet
{
    public class PlanetEntityConfiguration : IEntityTypeConfiguration<Domain.Models.PlanetModel.Base.Planet>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.PlanetModel.Base.Planet> builder)
        {
            builder.HasData(
                new AvypsoPlanet()
                {
                    Id = 1
                },
                new C137EarthPlanet()
                {
                    Id = 2
                },
                new CribatunePlanet()
                {
                    Id = 3
                },
                new DarvisPlanet()
                {
                    Id = 4
                },
                new DillonPlanet()
                {
                    Id = 5
                },
                new GingeriaPlanet()
                {
                    Id = 6
                },
                new HeolaraPlanet()
                {
                    Id = 7
                },
                new NusobosPlanet()
                {
                    Id = 8
                },
                new SidataniaPlanet()
                {
                    Id = 9
                },
                new YoiphusPlanet()
                {
                    Id = 10
                },
                new ZuccarsPlanet()
                {
                    Id = 11
                }
            );
        }
    }
}
