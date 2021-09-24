using GalacticEmpire.Domain.Models.EventModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.Event
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<Domain.Models.EventModel.Base.Event>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.EventModel.Base.Event> builder)
        {
            builder.HasData(
                new BadHarvestEvent()
                {
                    Id = 1
                },
                new GoodHarvestEvent()
                {
                    Id = 2
                },
                new JackpotEvent()
                {
                    Id = 3
                },
                new SatisfiedPeopleEvent()
                {
                    Id = 4
                },
                new UnsatisfiedPeopleEvent()
                {
                    Id = 5
                },
                new SatisfiedUnitsEvent()
                {
                    Id = 6
                },
                new UnsatisfiedUnitsEvent()
                {
                    Id = 7
                }
            );
        }
    }
}