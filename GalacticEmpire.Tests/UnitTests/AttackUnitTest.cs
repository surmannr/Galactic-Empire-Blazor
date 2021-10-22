using FluentAssertions;
using GalacticEmpire.Application.Features.Attack.Commands;
using GalacticEmpire.Application.Features.Attack.EventHandlers;
using GalacticEmpire.Application.Features.Attack.Events;
using GalacticEmpire.Application.Features.Attack.Queries;
using GalacticEmpire.Application.Features.Empire.Queries;
using GalacticEmpire.Application.SignalR;
using GalacticEmpire.Domain.Models.Activities;
using GalacticEmpire.Domain.Models.AttackModel;
using GalacticEmpire.Domain.Models.AttackModel.Base;
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
    public class AttackUnitTest : UnitTest
    {
        public AttackUnitTest() : base() { }

        [Fact]
        public async Task GetAttackReportPreviewTest()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange
            var attack = new Attack
            {
                Date = DateTimeOffset.Now,
                AttackerId = LoggedInEmpireId,
                DefenderId = defenderId,
                AttackUnits = new List<AttackUnit>()
                {
                    new AttackUnit
                    {
                        Amount = 10,
                        Level = 1,
                        UnitId = 1
                    }
                },
                DefenseUnits = new List<DefenseUnit>()
                {
                    new DefenseUnit
                    {
                        Amount = 10,
                        Level = 1,
                        UnitId = 1
                    }
                },
                WinnerId = LoggedInEmpireId
            };

            _dbContext.Attacks.Add(attack);

            await _dbContext.SaveChangesAsync();

            // Act
            var query = new GetAttackReportsPreviewQuery.Query
            {
                Filter = "",
                PaginationData = new Application.PaginationExtensions.PaginationData
                {
                    PageSize = 10,
                    PageNumber = 1
                }
            };

            var handler = new GetAttackReportsPreviewQuery.Handler(_dbContext, identityService);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            result.PageSize.Should().Be(query.PaginationData.PageSize);
            result.PageNumber.Should().Be(query.PaginationData.PageNumber);
            result.AllResultsCount.Should().Be(1);
            result.Results.Count().Should().Be(1);
        }

        [Fact]
        public async Task GetAttackReportDetailsTest()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange
            var attack = new Attack
            {
                Date = DateTimeOffset.Now,
                AttackerId = LoggedInEmpireId,
                DefenderId = defenderId,
                AttackUnits = new List<AttackUnit>()
                {
                    new AttackUnit
                    {
                        Amount = 10,
                        Level = 1,
                        UnitId = 1
                    }
                },
                DefenseUnits = new List<DefenseUnit>()
                {
                    new DefenseUnit
                    {
                        Amount = 10,
                        Level = 1,
                        UnitId = 1
                    }
                },
                WinnerId = LoggedInEmpireId
            };

            var attackReport = _dbContext.Attacks.Add(attack);

            await _dbContext.SaveChangesAsync();

            // Act
            var query = new GetAttackReportDetailsQuery.Query
            {
                AttackId = attackReport.Entity.Id,
            };

            var handler = new GetAttackReportDetailsQuery.Handler(_dbContext, identityService, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            result.Date.Should().Be(attack.Date);
            result.AttackUnits.Count.Should().Be(1);
            result.DefenseUnits.Count.Should().Be(1);
            result.WinnerEmpireId.Should().Be(attack.WinnerId);
        }

        [Fact]
        public async Task AttackEventHandlerTest()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange
            var attack = new Attack
            {
                Date = DateTimeOffset.Now,
                AttackerId = LoggedInEmpireId,
                DefenderId = defenderId,
                AttackUnits = new List<AttackUnit>()
                {
                    new AttackUnit
                    {
                        Amount = 10,
                        Level = 1,
                        UnitId = 1
                    }
                },
                DefenseUnits = new List<DefenseUnit>()
                {
                    new DefenseUnit
                    {
                        Amount = 10,
                        Level = 1,
                        UnitId = 1
                    }
                },
                WinnerId = LoggedInEmpireId
            };

            // Act
            var attackEvent = new AttackTimingEvent
            {
                ConnectionId = "",
                Attack = attack,
            };

            var mockHubService = new Mock<IGameHubService>();
            var eventhandler = new AttackTimingEventHandler(_dbContext, mockHubService.Object);
            await eventhandler.Handle(attackEvent, CancellationToken.None);

            // Act
            var actualAttacks = await _dbContext.Attacks.Where(a => a.AttackerId == LoggedInEmpireId).ToListAsync();

            actualAttacks.Count.Should().Be(1);
        }

        [Fact]
        public async Task SendAttackTest_ActiveFail()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange
            var activeAttack = new ActiveAttacking
            {
                DefenderEmpireName = "teszt",
                EmpireId = LoggedInEmpireId,
                EndDate = DateTime.Now.AddDays(1),
            };

            _dbContext.ActiveAttackings.Add(activeAttack);

            await _dbContext.SaveChangesAsync();

            // Act
            var query = new SendAttackCommand.Command
            {
                ConnectionId = "",
                SendAttackDto = new Shared.Dto.Attack.SendAttackDto
                {
                    AttackedEmpireId = defenderId,
                    Units = new List<SendAttackUnitDto>()
                    {
                        new SendAttackUnitDto
                        {
                            UnitId = 1,
                            Count = 2,
                            Level = 2
                        }
                    }
                }
            };

            var mockMediator = new Mock<IMediator>();
            var handler = new SendAttackCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InProcessException>()
                .WithMessage("Folyamatban van egy támadás.");
        }

        [Fact]
        public async Task SendAttackTest_SelfAttackFail()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange

            // Act
            var query = new SendAttackCommand.Command
            {
                ConnectionId = "",
                SendAttackDto = new Shared.Dto.Attack.SendAttackDto
                {
                    AttackedEmpireId = LoggedInEmpireId,
                    Units = new List<SendAttackUnitDto>()
                    {
                        new SendAttackUnitDto
                        {
                            UnitId = 1,
                            Count = 2,
                            Level = 2
                        }
                    }
                }
            };

            var mockMediator = new Mock<IMediator>();
            var handler = new SendAttackCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("Nem támadhatod meg magadat!");
        }

        [Fact]
        public async Task SendAttackTest_NotExistEmpireFail()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange

            // Act
            var query = new SendAttackCommand.Command
            {
                ConnectionId = "",
                SendAttackDto = new Shared.Dto.Attack.SendAttackDto
                {
                    AttackedEmpireId = Guid.NewGuid(),
                    Units = new List<SendAttackUnitDto>()
                    {
                        new SendAttackUnitDto
                        {
                            UnitId = 1,
                            Count = 2,
                            Level = 2
                        }
                    }
                }
            };

            var mockMediator = new Mock<IMediator>();
            var handler = new SendAttackCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Nincs ilyen birodalom, amit megtámadnál.");
        }

        [Fact]
        public async Task SendAttackTest_NotEnoughUnitsFail()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange

            // Act
            var query = new SendAttackCommand.Command
            {
                ConnectionId = "",
                SendAttackDto = new Shared.Dto.Attack.SendAttackDto
                {
                    AttackedEmpireId = defenderId,
                    Units = new List<SendAttackUnitDto>()
                    {
                        new SendAttackUnitDto
                        {
                            UnitId = 1,
                            Count = 2,
                            Level = 2
                        }
                    }
                }
            };

            var mockMediator = new Mock<IMediator>();
            var handler = new SendAttackCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("Nem áll rendelkezésre ennyi egység.");
        }

        [Fact]
        public async Task SendAttackTest_DroneAttackFail()
        {
            var defenderId = Guid.Parse("392a9574-11a7-4f01-1111-4980933cc7a6");

            // Arrange

            // Act
            var query = new SendAttackCommand.Command
            {
                ConnectionId = "",
                SendAttackDto = new Shared.Dto.Attack.SendAttackDto
                {
                    AttackedEmpireId = defenderId,
                    Units = new List<SendAttackUnitDto>()
                    {
                        new SendAttackUnitDto
                        {
                            UnitId = 5,
                            Count = 2,
                            Level = 2
                        }
                    }
                }
            };

            var mockMediator = new Mock<IMediator>();
            var handler = new SendAttackCommand.Handler(_dbContext, identityService, mockMediator.Object);

            Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidActionException>()
                .WithMessage("Nem lehet támadásba küldeni a felfedező drónt.");
        }
    }
}
