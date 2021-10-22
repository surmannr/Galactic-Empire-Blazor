using FluentAssertions;
using GalacticEmpire.Application.Features.Empire.Queries;
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
    public class EmpireUnitEvent : UnitTest
    {
        public EmpireUnitEvent() : base() {  }

        [Fact]
        public async Task GetEmpireDetailsTest()
        {
            // Arrange
            var empire = await _dbContext.Empires.Where(e => e.Id == LoggedInEmpireId)
                .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.Unit)
                    .Include(e => e.Alliance)
                        .ThenInclude(e => e.Alliance)
                    .Include(e => e.EmpirePlanets)
                        .ThenInclude(e => e.EmpirePlanetUpgrades)
                            .ThenInclude(e => e.Upgrade)
                    .Include(e => e.EmpireMaterials)
                        .ThenInclude(e => e.Material)
                    .Include(e => e.EmpirePlanets)
                        .ThenInclude(e => e.Planet)
                            .ThenInclude(e => e.PlanetProperty)
                    .Include(e => e.EmpireEvents)
                        .ThenInclude(e => e.Event)
                    .Include(e => e.Owner)
                .FirstOrDefaultAsync();

            // Act
            var query = new GetEmpireDetailsQuery.Query { };

            var handler = new GetEmpireDetailsQuery.Handler(_dbContext, identityService, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Act
            result.AllianceName.Should().Be(empire?.Alliance?.Alliance?.Name);
            result.AllianceInvitationRight.Should().Be(empire?.Alliance?.InvitationRight);
            result.Planets.Count().Should().Be(empire.EmpirePlanets.Count);
            result.Units.Count().Should().Be(empire.EmpireUnits.Count);
            result.Materials.Count().Should().Be(empire.EmpireMaterials.Count);
            result.IsAllianceLeader.Should().Be(empire?.Alliance?.IsLeader);
            result.MaxNumberOfPopulation.Should().Be(empire.MaxNumberOfPopulation);
            result.MaxNumberOfUnits.Should().Be(empire.MaxNumberOfUnits);
            result.Name.Should().Be(empire.Name);
            result.Id.Should().Be(empire.Id);
            result.Population.Should().Be(empire.Population);
            result.Event.Should().BeNull();
        }
    }
}
