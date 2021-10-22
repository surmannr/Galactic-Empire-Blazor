using FluentAssertions;
using GalacticEmpire.Application.Features.Event.Commands;
using GalacticEmpire.Application.Features.Event.Queries;
using GalacticEmpire.Domain.Models.EmpireModel;
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
    public class EventUnitTest : UnitTest
    {
        public EventUnitTest() : base() { }

        [Fact]
        public async Task GetAllEventsTest()
        {
            // Arrange
            var events = await _dbContext.Events.ToListAsync();

            // Act
            var query = new GetAllEventsQuery.Query { };

            var handler = new GetAllEventsQuery.Handler(_dbContext, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            result.Count.Should().Be(events.Count);
        }

        [Fact]
        public async Task GetAllUserEventsTest()
        {
            // Arrange
            var empireEvent = new EmpireEvent
            {
                Date = DateTimeOffset.Now,
                EmpireId = LoggedInEmpireId,
                EventId = 1,
            };

            _dbContext.EmpireEvents.Add(empireEvent);

            await _dbContext.SaveChangesAsync();

            var events = await _dbContext.EmpireEvents
                .Where(e => e.EmpireId == LoggedInEmpireId)
                .ToListAsync();

            // Act
            var query = new GetAllUserEventsPagedQuery.Query { };

            var handler = new GetAllUserEventsPagedQuery.Handler(_dbContext, _mapper, identityService);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            result.Count.Should().Be(events.Count);
        }

        [Fact]
        public async Task AddRandomEventTest()
        {
            // Act
            var query = new AddRandomEventToEmpiresCommand.Command {
                RandomEvent = new Shared.Dto.Event.RandomEventDto
                {
                    Seed = 100
                }
            };

            var handler = new AddRandomEventToEmpiresCommand.Handler(_dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            var empireevent = await _dbContext.EmpireEvents
                .Where(e => e.EmpireId == LoggedInEmpireId)
                .ToListAsync();

            empireevent.Count.Should().Be(1);
        }
    }
}
