using FluentAssertions;
using GalacticEmpire.Application.Features.Planet.Commands;
using GalacticEmpire.Application.Features.Planet.Event;
using GalacticEmpire.Application.Features.Planet.EventHandlers;
using GalacticEmpire.Application.Features.Planet.Queries;
using GalacticEmpire.Application.SignalR;
using GalacticEmpire.Domain.Models.Activities;
using GalacticEmpire.Shared.Constants.Planet;
using GalacticEmpire.Shared.Dto.Planet;
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
    public class PlanetUnitTest : UnitTest
    {
        public PlanetUnitTest() : base() { }

        [Fact]
        public async Task GetPlanetDetailsTest()
        {
            // Arrange
            var expectedPlanetDetails = new PlanetDetailsDto
            {
                Id = 1,
                CapturingTime = new TimeDto
                {
                    Hour = 0,
                    Minute = 5,
                    Second = 0,
                },
                Description = PlanetDescriptionConstants.Avypso_Description,
                IsCaptured = false,
                Name = PlanetEnum.Avypso.GetDisplayName(),
                PlanetProperty = new PlanetPropertyDto
                {
                    BaseFood = PlanetEffectConstants.Avypso_BaseFood,
                    BaseBitcoin = PlanetEffectConstants.Avypso_BaseBitcoin,
                    BaseQuartz = PlanetEffectConstants.Avypso_BaseQuartz,
                    MaxPopulationCount = PlanetEffectConstants.Avypso_MaxPopulationCount,
                    MaxUnitCount =  PlanetEffectConstants.Avypso_MaxUnitCount,
                    PlanetId = 1,
                }
            };

            // Act
            var query = new GetPlanetDetailsQuery.Query
            {
                Id = 1,
            };

            var handler = new GetPlanetDetailsQuery.Handler(_dbContext, _mapper);

            var actualPlanetDetails = await handler.Handle(query, CancellationToken.None);

            // Assert
            actualPlanetDetails.CapturingTime.Hour.Should().Be(expectedPlanetDetails.CapturingTime.Hour);
            actualPlanetDetails.CapturingTime.Minute.Should().Be(expectedPlanetDetails.CapturingTime.Minute);
            actualPlanetDetails.CapturingTime.Second.Should().Be(expectedPlanetDetails.CapturingTime.Second);
            actualPlanetDetails.Name.Should().Be(expectedPlanetDetails.Name);
            actualPlanetDetails.Description.Should().Be(expectedPlanetDetails.Description);
            actualPlanetDetails.Id.Should().Be(expectedPlanetDetails.Id);
            actualPlanetDetails.PlanetProperty.BaseBitcoin.Should().Be(expectedPlanetDetails.PlanetProperty.BaseBitcoin);
            actualPlanetDetails.PlanetProperty.BaseFood.Should().Be(expectedPlanetDetails.PlanetProperty.BaseFood);
            actualPlanetDetails.PlanetProperty.BaseQuartz.Should().Be(expectedPlanetDetails.PlanetProperty.BaseQuartz);
            actualPlanetDetails.PlanetProperty.MaxPopulationCount.Should().Be(expectedPlanetDetails.PlanetProperty.MaxPopulationCount);
            actualPlanetDetails.PlanetProperty.MaxUnitCount.Should().Be(expectedPlanetDetails.PlanetProperty.MaxUnitCount);
        }

        [Fact]
        public async Task GetAllPlanetsTest()
        {
            // Arrange
            var expectedAllPlanets = await _dbContext.Planets.ToListAsync();

            // Act
            var query = new GetAllPlanetsQuery.Query { };

            var handler = new GetAllPlanetsQuery.Handler(_dbContext, _mapper);

            var actualAllPlanets = await handler.Handle(query, CancellationToken.None);

            // Assert
            actualAllPlanets.Count.Should().Be(expectedAllPlanets.Count);
        }

        [Fact]
        public async Task GetAllCapturablePlanetsTest()
        {
            int capturedPlanetId = 1;

            // Arrange
            var expectedAllPlanets = await _dbContext.Planets.Where(p => p.Id != capturedPlanetId).ToListAsync();

            // Act

            var buyPlanet = new BuyPlanetTimingEvent
            {
                ConnectionId = "",
                EmpireId = LoggedInEmpireId,
                PlanetId = capturedPlanetId
            };

            var mockHubService = new Mock<IGameHubService>();
            var eventhandler = new BuyPlanetTimingEventHandler(_dbContext,mockHubService.Object);
            await eventhandler.Handle(buyPlanet,CancellationToken.None);

            var query = new GetAllCapturablePlanetsQuery.Query { };

            var handler = new GetAllCapturablePlanetsQuery.Handler(_dbContext, identityService ,_mapper);

            var actualAllPlanets = await handler.Handle(query, CancellationToken.None);

            // Assert
            actualAllPlanets.Count.Should().Be(expectedAllPlanets.Count);
        }

        [Fact]
        public async Task BuyPlanetEventHandlerTest()
        {
            int capturedPlanetId = 1;

            // Arrange
            var expectedAllPlanets = await _dbContext.Planets.Where(p => p.Id != capturedPlanetId).ToListAsync();

            // Act
            var buyPlanet = new BuyPlanetTimingEvent
            {
                ConnectionId = "",
                EmpireId = LoggedInEmpireId,
                PlanetId = capturedPlanetId
            };

            var mockHubService = new Mock<IGameHubService>();
            var eventhandler = new BuyPlanetTimingEventHandler(_dbContext, mockHubService.Object);
            await eventhandler.Handle(buyPlanet, CancellationToken.None);

            var query = new GetAllCapturablePlanetsQuery.Query { };

            var handler = new GetAllCapturablePlanetsQuery.Handler(_dbContext, identityService, _mapper);

            var actualAllPlanets = await handler.Handle(query, CancellationToken.None);

            // Assert
            actualAllPlanets.Any(p => p.Id == capturedPlanetId).Should().Be(false);
        }

        [Fact]
        public async Task BuyPlanetCheckTest_ActiveFail()
        {
            int capturedPlanetId = 1;

            // Arrange
            var active = new ActiveCapturing
            {
                EmpireId = LoggedInEmpireId,
                EndDate = DateTimeOffset.Now.AddDays(1),
                PlanetName = "Föld"
            };
            _dbContext.ActiveCapturings.Add(active);
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new BuyPlanetCommand.Command 
            {
                BuyPlanet = new BuyPlanetDto
                {
                    PlanetId = capturedPlanetId
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyPlanetCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InProcessException>()
                .WithMessage("Folyamatban van egy bolygófoglalás.");
        }

        [Fact]
        public async Task BuyPlanetCheckTest_DuplicateFail()
        {
            int capturedPlanetId = 1;

            // Arrange
            var buyPlanet = new BuyPlanetTimingEvent
            {
                ConnectionId = "",
                EmpireId = LoggedInEmpireId,
                PlanetId = capturedPlanetId
            };

            var mockHubService = new Mock<IGameHubService>();
            var eventhandler = new BuyPlanetTimingEventHandler(_dbContext, mockHubService.Object);
            await eventhandler.Handle(buyPlanet, CancellationToken.None);

            // Act
            var query = new BuyPlanetCommand.Command
            {
                BuyPlanet = new BuyPlanetDto
                {
                    PlanetId = capturedPlanetId
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyPlanetCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("Már van ilyen bolygód.");
        }

        [Fact]
        public async Task BuyPlanetCheckTest_NotExistFail()
        {
            // Arrange
            int capturedPlanetId = 100;

            // Act
            var query = new BuyPlanetCommand.Command
            {
                BuyPlanet = new BuyPlanetDto
                {
                    PlanetId = capturedPlanetId
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyPlanetCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Nem létezik ilyen bolygó.");
        }

        [Fact]
        public async Task BuyPlanetCheckTest_NotEnoughMaterialFail()
        {
            // Arrange
            int capturedPlanetId = 1;

            var empire = await _dbContext.Empires.Include(e => e.EmpireMaterials).FirstAsync(e => e.Id == LoggedInEmpireId);
            foreach(var material in empire.EmpireMaterials)
            {
                material.Amount = 0;
            }
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new BuyPlanetCommand.Command
            {
                BuyPlanet = new BuyPlanetDto
                {
                    PlanetId = capturedPlanetId
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyPlanetCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("Nincs elegendő nyersanyag!");
        }
    }
}
