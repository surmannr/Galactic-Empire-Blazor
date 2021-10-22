using FluentAssertions;
using GalacticEmpire.Application.Features.Unit.Commands;
using GalacticEmpire.Application.Features.Unit.EventHandlers;
using GalacticEmpire.Application.Features.Unit.Events;
using GalacticEmpire.Application.Features.Unit.Queries;
using GalacticEmpire.Application.SignalR;
using GalacticEmpire.Domain.Models.Activities;
using GalacticEmpire.Shared.Dto.Unit;
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
    public class UnitUnitTest : UnitTest
    {
        public UnitUnitTest() : base() { }

        [Fact]
        public async Task GetAllUnitsTest()
        {
            // Arrange
            var expectedAllUnits = await _dbContext.Units.ToListAsync();

            // Act
            var query = new GetAllUnitsQuery.Query { };

            var handler = new GetAllUnitsQuery.Handler(_dbContext, _mapper);

            var actualAllUnits = await handler.Handle(query, CancellationToken.None);

            // Assert
            actualAllUnits.Count.Should().Be(expectedAllUnits.Count);
        }

        [Fact]
        public async Task UnitTrainingEventHandlerTest()
        {
            // Arrange
            int unitId1 = 2;
            int unitId2 = 4;
            int amount1 = 10;
            int amount2 = 15;
            int level1 = 2;
            int level2 = 3;

            // Act
            var buyUnits = new UnitTrainingTimeEvent
            {
                ConnectionId = "",
                EmpireId = LoggedInEmpireId,
                UnitsCollection = new Shared.Dto.Unit.BuyUnitsCollectionDto
                {
                    Units = new List<BuyUnitDetailsDto>
                    {
                        new BuyUnitDetailsDto
                        {
                            UnitId = unitId1,
                            Count = amount1,
                            Level = level1
                        },
                        new BuyUnitDetailsDto
                        {
                            UnitId = unitId2,
                            Count = amount2,
                            Level = level2
                        }
                    }
                }
            };

            var mockHubService = new Mock<IGameHubService>();
            var eventhandler = new UnitTrainingTimeEventHandler(_dbContext, mockHubService.Object);
            await eventhandler.Handle(buyUnits, CancellationToken.None);

            var empireUnit1 = await _dbContext.EmpireUnits
                .FirstOrDefaultAsync(a => a.EmpireId == LoggedInEmpireId && a.UnitId == unitId1 && a.Level == level1);

            var empireUnit2 = await _dbContext.EmpireUnits
                .FirstOrDefaultAsync(a => a.EmpireId == LoggedInEmpireId && a.UnitId == unitId2 && a.Level == level2);

            // Assert
            empireUnit1.Amount.Should().Be(amount1);
            empireUnit2.Amount.Should().Be(amount2);
        }

        [Fact]
        public async Task BuyUnitCheckTest_ActiveFail()
        {
            int unitId1 = 2;
            int amount1 = 10;
            int level1 = 2;

            // Arrange
            var active = new ActiveTraining
            {
                EmpireId = LoggedInEmpireId,
                EndDate = DateTimeOffset.Now.AddDays(1),
                UnitAmount = amount1,
                UnitLevel = level1,
                UnitName = "Űrcirkáló"
            };
            _dbContext.ActiveTrainings.Add(active);
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new BuyUnitsCommand.Command
            {
                UnitsCollection = new BuyUnitsCollectionDto
                {
                    Units = new List<BuyUnitDetailsDto>
                    {
                        new BuyUnitDetailsDto
                        {
                            Count = amount1,
                            Level = level1,
                            UnitId = unitId1,
                        }
                    }
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyUnitsCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InProcessException>()
                .WithMessage("Folyamatban van egy egységképzés.");
        }

        [Fact]
        public async Task BuyUnitCheckTest_MaxCountFail()
        {
            int unitId1 = 2;
            int amount1 = 10;
            int level1 = 2;

            int unitId2 = 3;
            int amount2 = 10;
            int level2 = 3;

            // Arrange
            var empire = await _dbContext.Empires.SingleAsync(e => e.Id == LoggedInEmpireId);
            empire.MaxNumberOfUnits = 10;
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new BuyUnitsCommand.Command
            {
                UnitsCollection = new BuyUnitsCollectionDto
                {
                    Units = new List<BuyUnitDetailsDto>
                    {
                        new BuyUnitDetailsDto
                        {
                            Count = amount1,
                            Level = level1,
                            UnitId = unitId1,
                        },
                         new BuyUnitDetailsDto
                        {
                            Count = amount2,
                            Level = level2,
                            UnitId = unitId2,
                        }
                    }
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyUnitsCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("A megvételre szánt egység mennyisége túl lépi a birodalom korlátját.");
        }

        [Fact]
        public async Task BuyUnitCheckTest_NotExistUnitFail()
        {
            // Arrange
            int unitId1 = 2;
            int amount1 = 10;
            int level1 = 2;

            int unitId2 = 30;
            int amount2 = 10;
            int level2 = 3;

            // Act
            var query = new BuyUnitsCommand.Command
            {
                UnitsCollection = new BuyUnitsCollectionDto
                {
                    Units = new List<BuyUnitDetailsDto>
                    {
                        new BuyUnitDetailsDto
                        {
                            Count = amount1,
                            Level = level1,
                            UnitId = unitId1,
                        },
                         new BuyUnitDetailsDto
                        {
                            Count = amount2,
                            Level = level2,
                            UnitId = unitId2,
                        }
                    }
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyUnitsCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Nem létezik ilyen egység!");
        }

        [Fact]
        public async Task BuyUnitCheckTest_NotEnoughMaterialFail()
        {
            // Arrange
            int unitId1 = 2;
            int amount1 = 10;
            int level1 = 2;

            int unitId2 = 3;
            int amount2 = 10;
            int level2 = 3;

            var empire = await _dbContext.Empires.Include(e => e.EmpireMaterials).FirstAsync(e => e.Id == LoggedInEmpireId);
            foreach (var material in empire.EmpireMaterials)
            {
                material.Amount = 0;
            }
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new BuyUnitsCommand.Command
            {
                UnitsCollection = new BuyUnitsCollectionDto
                {
                    Units = new List<BuyUnitDetailsDto>
                    {
                        new BuyUnitDetailsDto
                        {
                            Count = amount1,
                            Level = level1,
                            UnitId = unitId1,
                        },
                         new BuyUnitDetailsDto
                        {
                            Count = amount2,
                            Level = level2,
                            UnitId = unitId2,
                        }
                    }
                },
                ConnectionId = ""
            };
            var mockMediator = new Mock<IMediator>();
            var handler = new BuyUnitsCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("Nincs elegendő nyersanyag!");
        }
    }
}
