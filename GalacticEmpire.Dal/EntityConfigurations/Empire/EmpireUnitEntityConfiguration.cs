using GalacticEmpire.Domain.Models.EmpireModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace GalacticEmpire.Dal.EntityConfigurations.Empire
{
    public class EmpireUnitEntityConfiguration : IEntityTypeConfiguration<EmpireUnit>
    {
        public void Configure(EntityTypeBuilder<EmpireUnit> builder)
        {
            var empireIds = new List<string>()
            {
                "af378505-14cb-4f49-1111-ba2c8fdef77d",
                "72ff37e8-5888-47c6-1111-15844a6449b1",
                "a63a97aa-4ae8-4185-1111-be02286b1542",
                "c4393fff-8d3a-4508-1111-794916e9e997",
                "cbbd70fb-06cd-4368-1111-93c237980d8c",
                "392a9574-11a7-4f01-1111-4980933cc7a6",
                "bf37d8cc-0744-4054-1111-603e6829799a",
                "488d40fe-e2c5-41e3-1111-dea16b7c2897",
                "0b62f843-4357-423b-1111-a2506ac91d5c",
                "c0b59d8d-58cc-4a54-a045-bf2a9341c658"
            };

            foreach(var empireId in empireIds)
            {
                for(int i = 1; i <= 4; i++)
                {
                    builder.HasData(
                        new EmpireUnit()
                        {
                            EmpireId = Guid.Parse(empireId),
                            Amount = 0,
                            UnitId = i,
                            Level = 1
                        },
                        new EmpireUnit()
                        {
                            EmpireId = Guid.Parse(empireId),
                            Amount = 0,
                            UnitId = i,
                            Level = 2
                        },
                        new EmpireUnit()
                        {
                            EmpireId = Guid.Parse(empireId),
                            Amount = 0,
                            UnitId = i,
                            Level = 3
                        }
                    );

                    builder.OwnsOne(e => e.FightPoint).HasData(
                        new
                        {
                            EmpireUnitEmpireId = Guid.Parse(empireId),
                            EmpireUnitUnitId = i,
                            EmpireUnitLevel = 1,
                            AttackPointMultiplier = 1.0,
                            DefensePointMultiplier = 1.0,
                            AttackPointBonus = 0,
                            DefensePointBonus = 0
                        },
                        new
                        {
                            EmpireUnitEmpireId = Guid.Parse(empireId),
                            EmpireUnitUnitId = i,
                            EmpireUnitLevel = 2,
                            AttackPointMultiplier = 1.0,
                            DefensePointMultiplier = 1.0,
                            AttackPointBonus = 0,
                            DefensePointBonus = 0
                        },
                        new
                        {
                            EmpireUnitEmpireId = Guid.Parse(empireId),
                            EmpireUnitUnitId = i,
                            EmpireUnitLevel = 3,
                            AttackPointMultiplier = 1.0,
                            DefensePointMultiplier = 1.0,
                            AttackPointBonus = 0,
                            DefensePointBonus = 0
                        }
                    );
                }

                builder.HasData(
                    new EmpireUnit()
                    {
                        EmpireId = Guid.Parse(empireId),
                        Amount = 0,
                        UnitId = 5,
                        Level = 1
                    }
                );

                builder.OwnsOne(e => e.FightPoint).HasData(
                    new
                    {
                        EmpireUnitEmpireId = Guid.Parse(empireId),
                        EmpireUnitUnitId = 5,
                        EmpireUnitLevel = 3,
                        AttackPointMultiplier = 1.0,
                        DefensePointMultiplier = 1.0,
                        AttackPointBonus = 0,
                        DefensePointBonus = 0
                    }
                );
            }
        }
    }
}
