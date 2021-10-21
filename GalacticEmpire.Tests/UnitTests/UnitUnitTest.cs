using FluentAssertions;
using GalacticEmpire.Application.Features.Unit.Queries;
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
    public class UnitUnitTest : UnitTest
    {
        public UnitUnitTest() : base() { }

        [Fact]
        public async Task GetAllUpgradesTest()
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
    }
}
