using GalacticEmpire.Domain.Models.UnitModel;
using GalacticEmpire.Shared.Constants;
using GalacticEmpire.Shared.Constants.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Dal.EntityConfigurations.Unit
{
    public class UnitLevelEntityConfiguration : IEntityTypeConfiguration<Domain.Models.UnitModel.UnitLevel>
    {
        public void Configure(EntityTypeBuilder<UnitLevel> builder)
        {
            builder.HasData(
                // SolarSail
                new UnitLevel
                {
                    Level = 1,
                    UnitId = 1,
                    AttackPoint = (int)(UnitsConstants.SolarSail_AttackPoint * UnitLevelConstants.OneLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.SolarSail_DefensePoint * UnitLevelConstants.OneLevelMultiplier),
                    TrainingTime = new TimeSpan(0,0, (int)(15 * UnitLevelConstants.OneLevelMultiplier))
                },
                new UnitLevel
                {
                    Level = 2,
                    UnitId = 1,
                    AttackPoint = (int)(UnitsConstants.SolarSail_AttackPoint * UnitLevelConstants.TwoLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.SolarSail_DefensePoint * UnitLevelConstants.TwoLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(15 * UnitLevelConstants.TwoLevelMultiplier))
                },
                new UnitLevel
                {
                    Level = 3,
                    UnitId = 1,
                    AttackPoint = (int)(UnitsConstants.SolarSail_AttackPoint * UnitLevelConstants.ThreeLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.SolarSail_DefensePoint * UnitLevelConstants.ThreeLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(15 * UnitLevelConstants.ThreeLevelMultiplier))
                },
                // SpaceCruiser
                new UnitLevel
                {
                    Level = 1,
                    UnitId = 2,
                    AttackPoint = (int)(UnitsConstants.SpaceCruiser_AttackPoint * UnitLevelConstants.OneLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.SpaceCruiser_DefensePoint * UnitLevelConstants.OneLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(20 * UnitLevelConstants.OneLevelMultiplier))
                },
                new UnitLevel
                {
                    Level = 2,
                    UnitId = 2,
                    AttackPoint = (int)(UnitsConstants.SpaceCruiser_AttackPoint * UnitLevelConstants.TwoLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.SpaceCruiser_DefensePoint * UnitLevelConstants.TwoLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(20 * UnitLevelConstants.TwoLevelMultiplier))
                },
                new UnitLevel
                {
                    Level = 3,
                    UnitId = 2,
                    AttackPoint = (int)(UnitsConstants.SpaceCruiser_AttackPoint * UnitLevelConstants.ThreeLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.SpaceCruiser_DefensePoint * UnitLevelConstants.ThreeLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(20 * UnitLevelConstants.ThreeLevelMultiplier))
                },
                // IronMan
                new UnitLevel
                {
                    Level = 1,
                    UnitId = 3,
                    AttackPoint = (int)(UnitsConstants.IronMan_AttackPoint * UnitLevelConstants.OneLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.IronMan_DefensePoint * UnitLevelConstants.OneLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(25 * UnitLevelConstants.OneLevelMultiplier))
                },
                new UnitLevel
                {
                    Level = 2,
                    UnitId = 3,
                    AttackPoint = (int)(UnitsConstants.IronMan_AttackPoint * UnitLevelConstants.TwoLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.IronMan_DefensePoint * UnitLevelConstants.TwoLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(25 * UnitLevelConstants.TwoLevelMultiplier))
                },
                new UnitLevel
                {
                    Level = 3,
                    UnitId = 3,
                    AttackPoint = (int)(UnitsConstants.IronMan_AttackPoint * UnitLevelConstants.ThreeLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.IronMan_DefensePoint * UnitLevelConstants.ThreeLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(25 * UnitLevelConstants.ThreeLevelMultiplier))
                },
                // MilleniumFalcon
                new UnitLevel
                {
                    Level = 1,
                    UnitId = 4,
                    AttackPoint = (int)(UnitsConstants.MilleniumFalcon_AttackPoint * UnitLevelConstants.OneLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.MilleniumFalcon_DefensePoint * UnitLevelConstants.OneLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(30 * UnitLevelConstants.OneLevelMultiplier))
                },
                new UnitLevel
                {
                    Level = 2,
                    UnitId = 4,
                    AttackPoint = (int)(UnitsConstants.MilleniumFalcon_AttackPoint * UnitLevelConstants.TwoLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.MilleniumFalcon_DefensePoint * UnitLevelConstants.TwoLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(30 * UnitLevelConstants.TwoLevelMultiplier))
                },
                new UnitLevel
                {
                    Level = 3,
                    UnitId = 4,
                    AttackPoint = (int)(UnitsConstants.MilleniumFalcon_AttackPoint * UnitLevelConstants.ThreeLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.MilleniumFalcon_DefensePoint * UnitLevelConstants.ThreeLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(30 * UnitLevelConstants.ThreeLevelMultiplier))
                },
                // ScoutDrone
                new UnitLevel
                {
                    Level = 1,
                    UnitId = 5,
                    AttackPoint = (int)(UnitsConstants.ScoutDrone_AttackPoint * UnitLevelConstants.OneLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.ScoutDrone_DefensePoint * UnitLevelConstants.OneLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(10 * UnitLevelConstants.OneLevelMultiplier))
                },
                new UnitLevel
                {
                    Level = 2,
                    UnitId = 5,
                    AttackPoint = (int)(UnitsConstants.ScoutDrone_AttackPoint * UnitLevelConstants.TwoLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.ScoutDrone_DefensePoint * UnitLevelConstants.TwoLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(10 * UnitLevelConstants.TwoLevelMultiplier))
                },
                new UnitLevel
                {
                    Level = 3,
                    UnitId = 5,
                    AttackPoint = (int)(UnitsConstants.ScoutDrone_AttackPoint * UnitLevelConstants.ThreeLevelMultiplier),
                    DefensePoint = (int)(UnitsConstants.ScoutDrone_DefensePoint * UnitLevelConstants.ThreeLevelMultiplier),
                    TrainingTime = new TimeSpan(0, 0, (int)(10 * UnitLevelConstants.ThreeLevelMultiplier))
                }
            );
        }
    }
}