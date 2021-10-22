using FluentAssertions;
using GalacticEmpire.Application.Features.Upgrade.Commands;
using GalacticEmpire.Application.Features.Upgrade.EventHandlers;
using GalacticEmpire.Application.Features.Upgrade.Events;
using GalacticEmpire.Application.Features.Upgrade.Queries;
using GalacticEmpire.Application.SignalR;
using GalacticEmpire.Domain.Models.Activities;
using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Shared.Dto.Time;
using GalacticEmpire.Shared.Enums.Planet;
using GalacticEmpire.Shared.Exceptions;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using GalacticEmpire.Tests.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GalacticEmpire.Tests.UnitTests
{
    public class UpgradeUnitTest : UnitTest
    {
        public UpgradeUnitTest() : base() { }

        [Fact]
        public async Task GetAllUpgradesTest()
        {
            // Arrange
            var expectedAllUpgrades = await _dbContext.Upgrades.ToListAsync();

            // Act
            var query = new GetAllUpgradesQuery.Query { };

            var handler = new GetAllUpgradesQuery.Handler(_dbContext, _mapper);

            var actualAllUpgrades = await handler.Handle(query, CancellationToken.None);

            // Assert
            actualAllUpgrades.Count.Should().Be(expectedAllUpgrades.Count);
        }

        [Fact]
        public async Task GetAllAvailableUpgradesTest()
        {
            int upgradeId = 1;
            int planetId = 1;
            Guid empirePlanetId = Guid.NewGuid();

            // Arrange
            var upgrades = await _dbContext.Upgrades.ToListAsync();
            _dbContext.EmpirePlanets.Add(new Domain.Models.EmpireModel.EmpirePlanet
            {
                EmpireId = LoggedInEmpireId,
                EmpirePlanetUpgrades = new List<EmpirePlanetUpgrade>() { 
                    new EmpirePlanetUpgrade
                    {
                        EmpirePlanetId = empirePlanetId,
                        UpgradeId = upgradeId,
                    }
                },
                Id = empirePlanetId,
                PlanetId = planetId
            });
            await _dbContext.SaveChangesAsync();
            var expectedEmpirePlanet = await _dbContext.EmpirePlanets
                .Include(e => e.EmpirePlanetUpgrades)
                .Where(p => p.Id == empirePlanetId).FirstOrDefaultAsync();

            // Act
            var query = new GetAvailableUpgradesForPlanet.Query { EmpirePlanetId = empirePlanetId };

            var handler = new GetAvailableUpgradesForPlanet.Handler(_dbContext, _mapper);

            var actualAvailableUpgrades = await handler.Handle(query, CancellationToken.None);

            // Assert
            actualAvailableUpgrades.Count.Should().Be(upgrades.Count - expectedEmpirePlanet.EmpirePlanetUpgrades.Count);
        }
        
        [Fact]
        public async Task BuyUpgradeEventHandlerTest()
        {
            int upgradeId = 1;
            int planetId = 1;
            Guid empirePlanetId = Guid.NewGuid();

            // Arrange
            _dbContext.EmpirePlanets.Add(new Domain.Models.EmpireModel.EmpirePlanet
            {
                EmpireId = LoggedInEmpireId,
                EmpirePlanetUpgrades = new List<EmpirePlanetUpgrade>(),
                Id = empirePlanetId,
                PlanetId = planetId
            });
            await _dbContext.SaveChangesAsync();
            var expectedEmpirePlanet = await _dbContext.EmpirePlanets
                .Include(e => e.EmpirePlanetUpgrades)
                .Where(p => p.Id == empirePlanetId).FirstOrDefaultAsync();

            // Act
            var buyUpgrade = new UpgradeTimingEvent
            {
                ConnectionId = "",
                EmpireId = LoggedInEmpireId,
                EmpirePlanetId = empirePlanetId,
                UpgradeId = upgradeId,
            };

            var mockHubService = new Mock<IGameHubService>();
            var eventhandler = new UpgradeTimingEventHandler(_dbContext, mockHubService.Object);
            await eventhandler.Handle(buyUpgrade, CancellationToken.None);

            var actualEmpirePlanet = await _dbContext.EmpirePlanets.Include(e => e.EmpirePlanetUpgrades)
                .FirstOrDefaultAsync(a => a.Id == empirePlanetId);

            // Assert
            actualEmpirePlanet.EmpirePlanetUpgrades.Count.Should().Be(expectedEmpirePlanet.EmpirePlanetUpgrades.Count);
        }
        
        [Fact]
        public async Task BuyUpgradeCheckTest_ActiveFail()
        {
            int upgradeId = 1;
            int planetId = 1;
            Guid empirePlanetId = Guid.NewGuid();

            // Arrange
            _dbContext.EmpirePlanets.Add(new Domain.Models.EmpireModel.EmpirePlanet
            {
                EmpireId = LoggedInEmpireId,
                EmpirePlanetUpgrades = new List<EmpirePlanetUpgrade>(),
                Id = empirePlanetId,
                PlanetId = planetId
            });
            await _dbContext.SaveChangesAsync();

            var active = new ActiveUpgrading
            {
                EmpireId = LoggedInEmpireId,
                EndDate = DateTimeOffset.Now.AddDays(1),
                UpgradeName = "Jó termés"
            };
            _dbContext.ActiveUpgradings.Add(active);
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new BuyUpgradeForPlanetCommand.Command
            {
                BuyUpgrade = new Shared.Dto.Upgrade.BuyUpgradeDto
                {
                    EmpirePlanetId = empirePlanetId,
                    UpgradeId = upgradeId,
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyUpgradeForPlanetCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InProcessException>()
                .WithMessage("Folyamatban van egy fejlesztés.");
        }
        
        [Fact]
        public async Task BuyUpgradeCheckTest_DuplicateFail()
        {
            int upgradeId = 1;
            int planetId = 1;
            Guid empirePlanetId = Guid.NewGuid();

            // Arrange
            _dbContext.EmpirePlanets.Add(new Domain.Models.EmpireModel.EmpirePlanet
            {
                EmpireId = LoggedInEmpireId,
                EmpirePlanetUpgrades = new List<EmpirePlanetUpgrade>() { 
                    new EmpirePlanetUpgrade
                    {
                        EmpirePlanetId=empirePlanetId,
                        UpgradeId= upgradeId,
                    }
                },
                Id = empirePlanetId,
                PlanetId = planetId
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new BuyUpgradeForPlanetCommand.Command
            {
                BuyUpgrade = new Shared.Dto.Upgrade.BuyUpgradeDto
                {
                    EmpirePlanetId = empirePlanetId,
                    UpgradeId = upgradeId,
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyUpgradeForPlanetCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("Már van ilyen fejlesztés a kiválasztott bolygón!");
        }
        
        [Fact]
        public async Task BuyUpgradeTest_NotExistFail()
        {
            int upgradeId = 100;
            int planetId = 1;
            Guid empirePlanetId = Guid.NewGuid();

            // Arrange
            _dbContext.EmpirePlanets.Add(new Domain.Models.EmpireModel.EmpirePlanet
            {
                EmpireId = LoggedInEmpireId,
                EmpirePlanetUpgrades = new List<EmpirePlanetUpgrade>(),
                Id = empirePlanetId,
                PlanetId = planetId
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new BuyUpgradeForPlanetCommand.Command
            {
                BuyUpgrade = new Shared.Dto.Upgrade.BuyUpgradeDto
                {
                    EmpirePlanetId = empirePlanetId,
                    UpgradeId = upgradeId,
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyUpgradeForPlanetCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Nem létezik ilyen fejlesztés.");
        }
        
        [Fact]
        public async Task BuyUpgradeTest_NotEnoughMaterialFail()
        {
            int upgradeId = 1;
            int planetId = 1;
            Guid empirePlanetId = Guid.NewGuid();

            // Arrange
            _dbContext.EmpirePlanets.Add(new Domain.Models.EmpireModel.EmpirePlanet
            {
                EmpireId = LoggedInEmpireId,
                EmpirePlanetUpgrades = new List<EmpirePlanetUpgrade>(),
                Id = empirePlanetId,
                PlanetId = planetId
            });
            await _dbContext.SaveChangesAsync();

            var empire = await _dbContext.Empires.Include(e => e.EmpireMaterials).FirstAsync(e => e.Id == LoggedInEmpireId);
            foreach (var material in empire.EmpireMaterials)
            {
                material.Amount = 0;
            }
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new BuyUpgradeForPlanetCommand.Command
            {
                BuyUpgrade = new Shared.Dto.Upgrade.BuyUpgradeDto
                {
                    EmpirePlanetId = empirePlanetId,
                    UpgradeId = upgradeId,
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyUpgradeForPlanetCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("Nincs elegendő nyersanyag!");
        }

        [Fact]
        public async Task BuyUpgradeTest_NotExistEmpirePlanetFail()
        {
            int upgradeId = 1;
            Guid empirePlanetId = Guid.NewGuid();

            // Arrange

            // Act
            var query = new BuyUpgradeForPlanetCommand.Command
            {
                BuyUpgrade = new Shared.Dto.Upgrade.BuyUpgradeDto
                {
                    EmpirePlanetId = empirePlanetId,
                    UpgradeId = upgradeId,
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyUpgradeForPlanetCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("Ez a bolygó, amihez a fejlesztést vásárolnád nincsen a birodalmadban.");
        }
    }
}
