using FluentAssertions;
using GalacticEmpire.Application.Features.User.Commands;
using GalacticEmpire.Application.Features.User.Queries;
using GalacticEmpire.Application.PaginationExtensions;
using GalacticEmpire.Shared.Constants;
using GalacticEmpire.Shared.Dto.User;
using GalacticEmpire.Tests.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    public class UserUnitTest : UnitTest
    {
        public UserUnitTest() : base() { }

        [Fact]
        public async Task GetUserByEmailAndUsernameTest()
        {
            // Arrange
            var loggedInUser = await _dbContext.Users.SingleAsync(u => u.Id == LoggedInUserId);

            // Act
            var query = new GetUserByEmailAndUsernameQuery.Query
            {
                UserName = loggedInUser.UserName,
                Email = loggedInUser.Email
            };

            var handler = new GetUserByEmailAndUsernameQuery.Handler(_dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Id.Should().Be(LoggedInUserId);
            result.Id.Should().Be(loggedInUser.Id);
        }

        [Fact]
        public async Task GetUserByEmailAndUsernameTest_NullResult()
        {
            // Act
            var query = new GetUserByEmailAndUsernameQuery.Query
            {
                UserName = "random",
                Email = "dolog"
            };

            var handler = new GetUserByEmailAndUsernameQuery.Handler(_dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task RanklistTest()
        {
            // Arrange
            var users = await _dbContext.Users.ToListAsync();

            // Act
            var query = new GetRankListQuery.Query
            {
                Filter = "",
                PaginationData = new PaginationData()
                {
                    PageSize = 5,
                    PageNumber = 1
                }
            };

            var handler = new GetRankListQuery.Handler(_dbContext, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.PageSize.Should().Be(5);
            result.PageNumber.Should().Be(1);
            result.AllResultsCount.Should().Be(users.Count);
            result.Results.Count().Should().Be(5);
        }

        [Fact]
        public async Task RanklistTest_Filter()
        {
            // Arrange
            var filteruser = await _dbContext.Users.SingleAsync(u => u.Id == LoggedInUserId);
            var users = await _dbContext.Users.ToListAsync();

            // Act
            var query = new GetRankListQuery.Query
            {
                Filter = filteruser.UserName,
                PaginationData = new PaginationData()
                {
                    PageSize = 5,
                    PageNumber = 1
                }
            };

            var handler = new GetRankListQuery.Handler(_dbContext, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.PageSize.Should().Be(5);
            result.PageNumber.Should().Be(1);
            result.AllResultsCount.Should().Be(1);
            result.Results.Count().Should().Be(1);
            result.Results.FirstOrDefault().Should().NotBeNull();
            result.Results.FirstOrDefault().UserName.Should().Be(filteruser.UserName);
        }

        [Fact]
        public async Task GetAttackableUsersTest()
        {
            // Arrange
            var filteruser = await _dbContext.Users.SingleAsync(u => u.Id == LoggedInUserId);
            var users = await _dbContext.Users.ToListAsync();

            // Act
            var query = new GetAttackableUsersQuery.Query
            {
                Filter = ""
            };

            var handler = new GetAttackableUsersQuery.Handler(_dbContext, identityService, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Count.Should().Be(users.Count - 1);
            result.Any(c => c.Id == LoggedInUserId).Should().BeFalse();
        }

        [Fact]
        public async Task GetAttackableUsersTest_Filter()
        {
            // Arrange
            var filteruser = await _dbContext.Users.FirstAsync(u => u.Id != LoggedInUserId);
            var users = await _dbContext.Users.ToListAsync();

            // Act
            var query = new GetAttackableUsersQuery.Query
            {
                Filter = filteruser.UserName
            };

            var handler = new GetAttackableUsersQuery.Handler(_dbContext, identityService, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Count.Should().Be(1);
            result.Any(c => c.Id == LoggedInUserId).Should().BeFalse();
            result.First(c => c.Id == filteruser.Id).UserName.Should().Be(filteruser.UserName);
        }

        [Fact]
        public async Task ChangeUsernameTest()
        {
            // Arrange
            var username = "teszt";

            var loggedinuser = await _dbContext.Users.SingleAsync(u => u.Id == LoggedInUserId);

            var oldusername = loggedinuser.UserName;

            // Act
            var query = new ChangeUsernameCommand.Command
            {
                NewUserName = username
            };

            var handler = new ChangeUsernameCommand.Handler(identityService,_dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            var modifieduser = await _dbContext.Users.SingleAsync(u => u.Id == LoggedInUserId);
            result.Should().BeTrue();
            modifieduser.UserName.Should().Be(username);
            modifieduser.UserName.Should().NotBe(oldusername);
        }

        [Fact]
        public async Task ChangeEmpirenameTest()
        {
            // Arrange
            var empirename = "teszt";

            var loggedinempire = _dbContext.Empires.Single(u => u.Id == LoggedInEmpireId);

            var oldempirename = loggedinempire.Name;

            // Act
            var query = new ChangeEmpirenameCommand.Command
            {
                NewEmpireName = empirename
            };

            var handler = new ChangeEmpirenameCommand.Handler(identityService, _dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            var modifiedempire = await _dbContext.Empires.SingleAsync(u => u.Id == LoggedInEmpireId);
            result.Should().BeTrue();
            modifiedempire.Name.Should().Be(empirename);
            modifiedempire.Name.Should().NotBe(oldempirename);
        }

        [Fact]
        public async Task ChangeEmailTest()
        {
            // Arrange
            var email = "teszt@teszt.hu";
            var loggedinuser = await _dbContext.Users.SingleAsync(u => u.Id == LoggedInUserId);
            var olduseremail = loggedinuser.Email;

            // Act
            var query = new ChangeEmailCommand.Command
            {
                NewEmail = email
            };

            var handler = new ChangeEmailCommand.Handler(identityService, _dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            var modifieduser = await _dbContext.Users.SingleAsync(u => u.Id == LoggedInUserId);
            result.Should().BeTrue();
            modifieduser.Email.Should().Be(email);
            modifieduser.Email.Should().NotBe(olduseremail);
        }

        [Fact]
        public async Task CalculatePointsTest()
        {
            // Arrange
            var loggedinempire = await _dbContext.Empires.SingleAsync(u => u.Id == LoggedInEmpireId);
            var populationPoints = loggedinempire.Population * PointConstants.Population;

            // Act
            var query = new CalculatePointsCommand.Command { };

            var handler = new CalculatePointsCommand.Handler(_dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            var modifieduser = await _dbContext.Users.SingleAsync(u => u.Id == LoggedInUserId);
            result.Should().BeTrue();
            modifieduser.Points.Should().Be(populationPoints);
        }
    }
}
