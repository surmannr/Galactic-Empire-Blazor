using GalacticEmpire.Shared.Constants.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GalacticEmpire.Dal.EntityConfigurations.User
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>() { UserId = "user1", RoleId = Roles.Admin },
                new IdentityUserRole<string>() { UserId = "user2", RoleId = Roles.User },
                new IdentityUserRole<string>() { UserId = "user3", RoleId = Roles.User },
                new IdentityUserRole<string>() { UserId = "user4", RoleId = Roles.User },
                new IdentityUserRole<string>() { UserId = "user5", RoleId = Roles.User },
                new IdentityUserRole<string>() { UserId = "user6", RoleId = Roles.User },
                new IdentityUserRole<string>() { UserId = "user7", RoleId = Roles.User },
                new IdentityUserRole<string>() { UserId = "user8", RoleId = Roles.User },
                new IdentityUserRole<string>() { UserId = "user9", RoleId = Roles.User },
                new IdentityUserRole<string>() { UserId = "user10", RoleId = Roles.User }
            );
        }
    }
}
