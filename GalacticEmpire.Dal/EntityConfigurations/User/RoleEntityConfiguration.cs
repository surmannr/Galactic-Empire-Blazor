using GalacticEmpire.Shared.Constants.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.User
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole()
                {
                    Name = Roles.Admin,
                    NormalizedName = Roles.Admin.ToUpper(),
                    Id = Roles.Admin,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole()
                {
                    Name = Roles.User,
                    NormalizedName = Roles.User.ToUpper(),
                    Id = Roles.User,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
        }
    }
}
