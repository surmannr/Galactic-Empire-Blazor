using AutoMapper;
using GalacticEmpire.Api.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.Mapper;
using GalacticEmpire.Dal;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace GalacticEmpire.Tests.Base
{
    public abstract class UnitTest : IDisposable
    {
        protected readonly SqliteConnection _connection;
        protected GalacticEmpireDbContext _dbContext;
        protected readonly IIdentityService identityService;
        protected IMapper _mapper;

        protected readonly string LoggedInUserId = "user1";
        protected readonly Guid LoggedInEmpireId = Guid.Parse("AF378505-14CB-4F49-1111-BA2C8FDEF77D");

        public UnitTest()
        {
            // Adatbázis kapcsolat

            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // Adatbázis

            var options = new DbContextOptionsBuilder<GalacticEmpireDbContext>()
                .UseSqlite(_connection)
                .Options;

            _dbContext = new GalacticEmpireDbContext(options);
            _dbContext.Database.EnsureCreated();

            // HttpContext

            var mockAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpContext = new Mock<HttpContext>();

            mockHttpContext.Setup(x => x.User.Claims).Returns(new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, LoggedInUserId)
            });

            mockAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);

            identityService = new IdentityService(mockAccessor.Object);

            // Automapper

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _connection.Dispose();
        }
    }
}
