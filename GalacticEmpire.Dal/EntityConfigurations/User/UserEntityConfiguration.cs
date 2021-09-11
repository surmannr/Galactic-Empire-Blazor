using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GalacticEmpire.Dal.EntityConfigurations.UserConfig
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<Domain.Models.UserModel.Base.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.UserModel.Base.User> builder)
        {
            PasswordHasher<Domain.Models.UserModel.Base.User> ph = new PasswordHasher<Domain.Models.UserModel.Base.User>();

            var user1 = new Domain.Models.UserModel.Base.User()
            {
                Id = "user1",
                UserName = "sstrahan0",
                NormalizedUserName = "SSTRAHAN0",
                Points = 0,
                EmpireId = Guid.Parse("af378505-14cb-4f49-1111-ba2c8fdef77d"),
                SecurityStamp = "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2",
                ConcurrencyStamp = "cfc830af-302f-44b7-a973-805e6439b2ad"
            };
            var user2 = new Domain.Models.UserModel.Base.User()
            {
                Id = "user2",
                UserName = "ltippin1",
                NormalizedUserName = "LTIPPIN1",
                Points = 0,
                EmpireId = Guid.Parse("72ff37e8-5888-47c6-1111-15844a6449b1"),
                SecurityStamp = "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2",
                ConcurrencyStamp = "cfc830af-302f-44b7-a973-805e6439b2ad"
            };
            var user3 = new Domain.Models.UserModel.Base.User()
            {
                Id = "user3",
                UserName = "blyptratt2",
                NormalizedUserName = "BLYPTRATT2",
                Points = 0,
                EmpireId = Guid.Parse("a63a97aa-4ae8-4185-1111-be02286b1542"),
                SecurityStamp = "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2",
                ConcurrencyStamp = "cfc830af-302f-44b7-a973-805e6439b2ad"
            };
            var user4 = new Domain.Models.UserModel.Base.User()
            {
                Id = "user4",
                UserName = "jmelior3",
                NormalizedUserName = "JMELIOR3",
                Points = 0,
                EmpireId = Guid.Parse("c4393fff-8d3a-4508-1111-794916e9e997"),
                SecurityStamp = "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2",
                ConcurrencyStamp = "cfc830af-302f-44b7-a973-805e6439b2ad"
            };
            var user5 = new Domain.Models.UserModel.Base.User()
            {
                Id = "user5",
                UserName = "tmaxworthy4",
                NormalizedUserName = "TMAXWORTHY4",
                Points = 0,
                EmpireId = Guid.Parse("cbbd70fb-06cd-4368-1111-93c237980d8c"),
                SecurityStamp = "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2",
                ConcurrencyStamp = "cfc830af-302f-44b7-a973-805e6439b2ad"
            };
            var user6 = new Domain.Models.UserModel.Base.User()
            {
                Id = "user6",
                UserName = "hcheverell5",
                NormalizedUserName = "HCHEVERELL5",
                Points = 0,
                EmpireId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6"),
                SecurityStamp = "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2",
                ConcurrencyStamp = "cfc830af-302f-44b7-a973-805e6439b2ad"
            };
            var user7 = new Domain.Models.UserModel.Base.User()
            {
                Id = "user7",
                UserName = "gboskell6",
                NormalizedUserName = "GBOSKELL6",
                Points = 0,
                EmpireId = Guid.Parse("bf37d8cc-0744-4054-1111-603e6829799a"),
                SecurityStamp = "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2",
                ConcurrencyStamp = "cfc830af-302f-44b7-a973-805e6439b2ad"
            };
            var user8 = new Domain.Models.UserModel.Base.User()
            {
                Id = "user8",
                UserName = "erylett7",
                NormalizedUserName = "ERYLETT7",
                Points = 0,
                EmpireId = Guid.Parse("488d40fe-e2c5-41e3-1111-dea16b7c2897"),
                SecurityStamp = "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2",
                ConcurrencyStamp = "cfc830af-302f-44b7-a973-805e6439b2ad"
            };
            var user9 = new Domain.Models.UserModel.Base.User()
            {
                Id = "user9",
                UserName = "kseely8",
                NormalizedUserName = "KSEELY8",
                Points = 0,
                EmpireId = Guid.Parse("0b62f843-4357-423b-1111-a2506ac91d5c"),
                SecurityStamp = "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2",
                ConcurrencyStamp = "cfc830af-302f-44b7-a973-805e6439b2ad"
            };
            var user10 = new Domain.Models.UserModel.Base.User()
            {
                Id = "user10",
                UserName = "hfilinkov9",
                NormalizedUserName = "HFILINKOV9",
                Points = 0,
                EmpireId = Guid.Parse("c0b59d8d-58cc-4a54-a045-bf2a9341c658"),
                SecurityStamp = "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2",
                ConcurrencyStamp = "cfc830af-302f-44b7-a973-805e6439b2ad"
            };

            user1.PasswordHash = ph.HashPassword(user1, "asd123ASD?");
            user2.PasswordHash = ph.HashPassword(user2, "asd123ASD?");
            user3.PasswordHash = ph.HashPassword(user3, "asd123ASD?");
            user4.PasswordHash = ph.HashPassword(user4, "asd123ASD?");
            user5.PasswordHash = ph.HashPassword(user5, "asd123ASD?");
            user6.PasswordHash = ph.HashPassword(user6, "asd123ASD?");
            user7.PasswordHash = ph.HashPassword(user7, "asd123ASD?");
            user8.PasswordHash = ph.HashPassword(user8, "asd123ASD?");
            user9.PasswordHash = ph.HashPassword(user9, "asd123ASD?");
            user10.PasswordHash = ph.HashPassword(user10, "asd123ASD?");

            builder.HasData(
                user1, user2, user3, user4, user5, user6, user7, user8, user9, user10
            );
        }
    }
}