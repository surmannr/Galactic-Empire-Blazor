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
                new AvypsoPlanet(1)
                {
                    Id = 1
                },
                new C137EarthPlanet(2)
                {
                    Id = 2
                },
                new CribatunePlanet(3)
                {
                    Id = 3
                },
                new DarvisPlanet(4)
                {
                    Id = 4
                },
                new DillonPlanet(5)
                {
                    Id = 5
                },
                new GingeriaPlanet(6)
                {
                    Id = 6
                },
                new HeolaraPlanet(7)
                {
                    Id = 7
                },
                new NusobosPlanet(8)
                {
                    Id = 8
                },
                new SidataniaPlanet(9)
                {
                    Id = 9
                },
                new YoiphusPlanet(10)
                {
                    Id = 10
                },
                new ZuccarsPlanet(11)
                {
                    Id = 11
                }
            );
        }
    }
}
