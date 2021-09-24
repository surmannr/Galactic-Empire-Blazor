using GalacticEmpire.Shared.Enums.Material;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.Material
{
    public class MaterialEntityConfiguration : IEntityTypeConfiguration<Domain.Models.MaterialModel.Base.Material>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.MaterialModel.Base.Material> builder)
        {
            builder.HasData(
                new Domain.Models.MaterialModel.Base.Material
                {
                    Id = 1,
                    Name = MaterialEnum.Quartz.GetDisplayName()
                },
                new Domain.Models.MaterialModel.Base.Material
                {
                    Id = 2,
                    Name = MaterialEnum.Food.GetDisplayName()
                },
                new Domain.Models.MaterialModel.Base.Material
                {
                    Id = 3,
                    Name = MaterialEnum.Bitcoin.GetDisplayName()
                }
            );
        }
    }
}