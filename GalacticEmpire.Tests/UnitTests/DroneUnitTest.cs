using FluentAssertions;
using GalacticEmpire.Application.Features.Drone.Commands;
using GalacticEmpire.Application.Features.Drone.EventHandlers;
using GalacticEmpire.Application.Features.Drone.Events;
using GalacticEmpire.Application.Features.Drone.Queries;
using GalacticEmpire.Application.Features.Empire.Queries;
using GalacticEmpire.Application.SignalR;
using GalacticEmpire.Domain.Models.Activities;
using GalacticEmpire.Domain.Models.AttackModel;
using GalacticEmpire.Shared.Exceptions;
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
    public class DroneUnitTest : UnitTest
    {
        public DroneUnitTest() : base() { }

        [Fact]
        public async Task GetDroneReportPreviewTest()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange
            var droneAttack = new DroneAttack
            {
                Date = DateTimeOffset.Now,
                AttackerId = LoggedInEmpireId,
                DefenderId = defenderId,
                DefenderDefensivePoints = 100,
                NumberOfAttackerDrones = 1000,
                NumberOfDefenderDrones = 1000,
                WinnerId = LoggedInEmpireId
            };

            _dbContext.DroneAttacks.Add(droneAttack);

            await _dbContext.SaveChangesAsync();

            // Act
            var query = new GetDroneReportsPreviewQuery.Query
            { 
                Filter = "",
                PaginationData = new Application.PaginationExtensions.PaginationData
                {
                    PageSize = 10,
                    PageNumber = 1
                }
            };

            var handler = new GetDroneReportsPreviewQuery.Handler(_dbContext, identityService);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            result.PageSize.Should().Be(query.PaginationData.PageSize);
            result.PageNumber.Should().Be(query.PaginationData.PageNumber);
            result.AllResultsCount.Should().Be(1);
            result.Results.Count().Should().Be(1);
        }

        [Fact]
        public async Task GetDroneReportDetailsTest()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange
            var droneAttack = new DroneAttack
            {
                Date = DateTimeOffset.Now,
                AttackerId = LoggedInEmpireId,
                DefenderId = defenderId,
                DefenderDefensivePoints = 100,
                NumberOfAttackerDrones = 1000,
                NumberOfDefenderDrones = 1000,
                WinnerId = LoggedInEmpireId
            };

            var droneReport = _dbContext.DroneAttacks.Add(droneAttack);

            await _dbContext.SaveChangesAsync();

            // Act
            var query = new GetDroneReportDetailsQuery.Query
            {
                DroneAttackId = droneReport.Entity.Id,
            };

            var handler = new GetDroneReportDetailsQuery.Handler(_dbContext, identityService, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            result.Date.Should().Be(droneAttack.Date);
            result.NumberOfAttackerDrones.Should().Be(droneAttack.NumberOfAttackerDrones);
            result.NumberOfDefenderDrones.Should().Be(droneAttack.NumberOfDefenderDrones);
            result.WinnerEmpireId.Should().Be(droneAttack.WinnerId);
        }

        [Fact]
        public async Task DroneEventHandlerTest()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange
            var droneAttack = new DroneAttack
            {
                Date = DateTimeOffset.Now,
                AttackerId = LoggedInEmpireId,
                DefenderId = defenderId,
                DefenderDefensivePoints = 100,
                NumberOfAttackerDrones = 1000,
                NumberOfDefenderDrones = 1000,
                WinnerId = LoggedInEmpireId
            };

            // Act
            var droneEvent = new DroneTimingEvent
            {
                ConnectionId = "",
                DroneAttack = droneAttack,
            };

            var mockHubService = new Mock<IGameHubService>();
            var eventhandler = new DroneTimingEventHandler(_dbContext, mockHubService.Object);
            await eventhandler.Handle(droneEvent, CancellationToken.None);

            // Act
            var actualDroneAttacks = await _dbContext.DroneAttacks.Where(a => a.AttackerId == LoggedInEmpireId).ToListAsync();

            actualDroneAttacks.Count.Should().Be(1);
        }

        [Fact]
        public async Task SendDroneTest_ActiveFail()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange
            var activeSpy = new ActiveSpying
            {
                DefenderEmpireName = "teszt",
                EmpireId = LoggedInEmpireId,
                EndDate = DateTime.Now.AddDays(1),
            };

            _dbContext.ActiveSpyings.Add(activeSpy);

            await _dbContext.SaveChangesAsync();

            // Act
            var query = new SendDroneAttackCommand.Command
            {
                ConnectionId = "",
                SendDrone = new Shared.Dto.Drone.SendDroneDto
                {
                    DronedEmpireId = defenderId,
                    NumberOfDrones = 100
                }
            };

            var mockMediator = new Mock<IMediator>();
            var handler = new SendDroneAttackCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InProcessException>()
                .WithMessage("Folyamatban van egy kémkedés.");
        }

        [Fact]
        public async Task SendDroneTest_SelfSpyFail()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange

            // Act
            var query = new SendDroneAttackCommand.Command
            {
                ConnectionId = "",
                SendDrone = new Shared.Dto.Drone.SendDroneDto
                {
                    DronedEmpireId = LoggedInEmpireId,
                    NumberOfDrones = 100
                }
            };

            var mockMediator = new Mock<IMediator>();
            var handler = new SendDroneAttackCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("Nem kémkedheted meg magadat!");
        }

        [Fact]
        public async Task SendDroneTest_NotExistEmpireFail()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange

            // Act
            var query = new SendDroneAttackCommand.Command
            {
                ConnectionId = "",
                SendDrone = new Shared.Dto.Drone.SendDroneDto
                {
                    DronedEmpireId = Guid.NewGuid(),
                    NumberOfDrones = 100
                }
            };

            var mockMediator = new Mock<IMediator>();
            var handler = new SendDroneAttackCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Nincs ilyen birodalom, amit kémkedhetnél.");
        }

        [Fact]
        public async Task SendDroneTest_NotEnoughDroneFail()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange

            // Act
            var query = new SendDroneAttackCommand.Command
            {
                ConnectionId = "",
                SendDrone = new Shared.Dto.Drone.SendDroneDto
                {
                    DronedEmpireId = defenderId,
                    NumberOfDrones = 100
                }
            };

            var mockMediator = new Mock<IMediator>();
            var handler = new SendDroneAttackCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Nincs elegendő drónod a kémkedéshez.");
        }
    }
}
