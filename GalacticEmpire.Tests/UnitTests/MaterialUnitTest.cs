using FluentAssertions;
using GalacticEmpire.Application.Features.Material.Commands;
using GalacticEmpire.Application.Features.Material.Queries;
using GalacticEmpire.Shared.Constants;
using GalacticEmpire.Shared.Enums.Material;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using GalacticEmpire.Tests.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GalacticEmpire.Tests.UnitTests
{
    public class MaterialUnitTest : UnitTest
    {
        public MaterialUnitTest() : base() { }

        [Fact]
        public async Task GetAllMaterialTest()
        {
            // Arrange
            var materials = await _dbContext.Materials.ToListAsync();

            // Act
            var query = new GetAllMaterialsQuery.Query { };

            var handler = new GetAllMaterialsQuery.Handler(_dbContext, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            result.Count.Should().Be(materials.Count);
        }

        [Fact]
        public async Task PayoffEmpireMercenariesAndFeedEveryoneTest()
        {
            // Arrange
            var empire = await _dbContext.Empires
                .Include(e => e.EmpireUnits)
                .Include(e => e.EmpireMaterials)
                    .ThenInclude(e => e.Material)
                .SingleAsync(e => e.Id == LoggedInEmpireId);
            empire.EmpireUnits.Single(e => e.UnitId == 1 && e.Level == 1 && e.EmpireId == LoggedInEmpireId).Amount = 10;
            var oldPopulationCount = empire.Population;
            await _dbContext.SaveChangesAsync();
            var empireFood = empire.EmpireMaterials.FirstOrDefault(e => e.Material.Name == MaterialEnum.Food.GetDisplayName()).Amount;
            var empireQuartz = empire.EmpireMaterials.FirstOrDefault(e => e.Material.Name == MaterialEnum.Quartz.GetDisplayName()).Amount;

            // Act
            var query = new PayoffEmpireMercenariesAndFeedEveryoneCommand.Command { };

            var handler = new PayoffEmpireMercenariesAndFeedEveryoneCommand.Handler(_dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            var modifiedEmpire = await _dbContext.Empires
                .Include(e => e.EmpireMaterials)
                    .ThenInclude(e => e.Material)
                .SingleAsync(e => e.Id == LoggedInEmpireId);

            result.Should().BeTrue();

            modifiedEmpire.EmpireMaterials
                .FirstOrDefault(e => e.Material.Name == MaterialEnum.Food.GetDisplayName())
                .Amount.Should().Be(empireFood - UnitsConstants.SolarSail_SupplyPerHour * 10);

            modifiedEmpire.EmpireMaterials
              .FirstOrDefault(e => e.Material.Name == MaterialEnum.Quartz.GetDisplayName())
              .Amount.Should().Be(empireQuartz - UnitsConstants.SolarSail_MercenaryPerHour * 10);

            modifiedEmpire.Population.Should().Be(oldPopulationCount + 10000);
        }

        [Fact]
        public async Task PayoffMaterialsForEmpiresTest()
        {
            // Arrange
            var empire = await _dbContext.Empires
                .Include(e => e.EmpireMaterials)
                    .ThenInclude(e => e.Material)
                .SingleAsync(e => e.Id == LoggedInEmpireId);

            var empireFood = empire.EmpireMaterials.FirstOrDefault(e => e.Material.Name == MaterialEnum.Food.GetDisplayName());
            var empireQuartz = empire.EmpireMaterials.FirstOrDefault(e => e.Material.Name == MaterialEnum.Quartz.GetDisplayName());
            var empireBitcoin = empire.EmpireMaterials.FirstOrDefault(e => e.Material.Name == MaterialEnum.Bitcoin.GetDisplayName());

            var oldFoodAmount = empireFood.Amount;
            var oldQuartzAmount = empireQuartz.Amount;
            var oldBitcoinAmount = empireBitcoin.Amount;

            // Act
            var query = new PayoffMaterialsForEmpiresCommand.Command { };

            var handler = new PayoffMaterialsForEmpiresCommand.Handler(_dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            var modifiedEmpire = await _dbContext.Empires
                .Include(e => e.EmpireMaterials)
                    .ThenInclude(e => e.Material)
                .SingleAsync(e => e.Id == LoggedInEmpireId);

            modifiedEmpire.EmpireMaterials.FirstOrDefault(e => e.Material.Name == MaterialEnum.Food.GetDisplayName())
                .Amount.Should().Be(oldFoodAmount + (int)(empireFood.BaseProduction * empireFood.ProductionMultiplier));

            modifiedEmpire.EmpireMaterials.FirstOrDefault(e => e.Material.Name == MaterialEnum.Quartz.GetDisplayName())
               .Amount.Should().Be(oldQuartzAmount + (int)(empireQuartz.BaseProduction * empireQuartz.ProductionMultiplier));

            modifiedEmpire.EmpireMaterials.FirstOrDefault(e => e.Material.Name == MaterialEnum.Bitcoin.GetDisplayName())
              .Amount.Should().Be(oldBitcoinAmount + (int)(empireBitcoin.BaseProduction * empireBitcoin.ProductionMultiplier));
        }
    }
}
