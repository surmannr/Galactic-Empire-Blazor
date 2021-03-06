using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GalacticEmpire.Dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveAttackings",
                columns: table => new
                {
                    EmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DefenderEmpireName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveAttackings", x => x.EmpireId);
                });

            migrationBuilder.CreateTable(
                name: "ActiveCapturings",
                columns: table => new
                {
                    EmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PlanetName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveCapturings", x => x.EmpireId);
                });

            migrationBuilder.CreateTable(
                name: "ActiveSpyings",
                columns: table => new
                {
                    EmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DefenderEmpireName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSpyings", x => x.EmpireId);
                });

            migrationBuilder.CreateTable(
                name: "ActiveTrainings",
                columns: table => new
                {
                    EmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitLevel = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UnitAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveTrainings", x => new { x.EmpireId, x.UnitName, x.UnitLevel });
                });

            migrationBuilder.CreateTable(
                name: "ActiveUpgradings",
                columns: table => new
                {
                    EmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpgradeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveUpgradings", x => x.EmpireId);
                });

            migrationBuilder.CreateTable(
                name: "Alliances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alliances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapturingTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MercenaryPerHour = table.Column<int>(type: "int", nullable: false),
                    SupplyPerHour = table.Column<int>(type: "int", nullable: false),
                    RankPoint = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Upgrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpgradeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpgradeTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upgrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empires",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxNumberOfUnits = table.Column<int>(type: "int", nullable: false),
                    MaxNumberOfPopulation = table.Column<int>(type: "int", nullable: false),
                    Population = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empires_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanetPriceMaterials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    PlanetId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanetPriceMaterials", x => new { x.PlanetId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_PlanetPriceMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanetPriceMaterials_Planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanetProperties",
                columns: table => new
                {
                    PlanetId = table.Column<int>(type: "int", nullable: false),
                    BaseFood = table.Column<int>(type: "int", nullable: false),
                    BaseQuartz = table.Column<int>(type: "int", nullable: false),
                    BaseBitcoin = table.Column<int>(type: "int", nullable: false),
                    MaxUnitCount = table.Column<int>(type: "int", nullable: false),
                    MaxPopulationCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanetProperties", x => x.PlanetId);
                    table.ForeignKey(
                        name: "FK_PlanetProperties_Planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitLevels",
                columns: table => new
                {
                    Level = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    AttackPoint = table.Column<int>(type: "int", nullable: false),
                    DefensePoint = table.Column<int>(type: "int", nullable: false),
                    TrainingTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitLevels", x => new { x.UnitId, x.Level });
                    table.ForeignKey(
                        name: "FK_UnitLevels_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitPriceMaterials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitPriceMaterials", x => new { x.UnitId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_UnitPriceMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitPriceMaterials_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UpgradePriceMaterials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    UpgradeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpgradePriceMaterials", x => new { x.UpgradeId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_UpgradePriceMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UpgradePriceMaterials_Upgrades_UpgradeId",
                        column: x => x.UpgradeId,
                        principalTable: "Upgrades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AllianceInvitations",
                columns: table => new
                {
                    AllianceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InviterEmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitedEmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllianceInvitations", x => new { x.AllianceId, x.InvitedEmpireId, x.InviterEmpireId });
                    table.ForeignKey(
                        name: "FK_AllianceInvitations_Alliances_AllianceId",
                        column: x => x.AllianceId,
                        principalTable: "Alliances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllianceInvitations_Empires_InvitedEmpireId",
                        column: x => x.InvitedEmpireId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllianceInvitations_Empires_InviterEmpireId",
                        column: x => x.InviterEmpireId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AllianceMembers",
                columns: table => new
                {
                    AllianceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitationRight = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsLeader = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllianceMembers", x => new { x.AllianceId, x.EmpireId });
                    table.ForeignKey(
                        name: "FK_AllianceMembers_Alliances_AllianceId",
                        column: x => x.AllianceId,
                        principalTable: "Alliances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllianceMembers_Empires_EmpireId",
                        column: x => x.EmpireId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AttackerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attacks_Empires_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attacks_Empires_DefenderId",
                        column: x => x.DefenderId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DroneAttacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfAttackerDrones = table.Column<int>(type: "int", nullable: false),
                    NumberOfDefenderDrones = table.Column<int>(type: "int", nullable: false),
                    DefenderDefensivePoints = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AttackerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneAttacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DroneAttacks_Empires_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DroneAttacks_Empires_DefenderId",
                        column: x => x.DefenderId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpireEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpireEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpireEvents_Empires_EmpireId",
                        column: x => x.EmpireId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpireEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpireMaterials",
                columns: table => new
                {
                    EmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    BaseProduction = table.Column<double>(type: "float", nullable: false),
                    ProductionMultiplier = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpireMaterials", x => new { x.EmpireId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_EmpireMaterials_Empires_EmpireId",
                        column: x => x.EmpireId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpireMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpirePlanets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanetId = table.Column<int>(type: "int", nullable: false),
                    EmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpirePlanets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpirePlanets_Empires_EmpireId",
                        column: x => x.EmpireId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpirePlanets_Planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpireUnits",
                columns: table => new
                {
                    Level = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    EmpireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    FightPoint_AttackPointMultiplier = table.Column<double>(type: "float", nullable: true),
                    FightPoint_DefensePointMultiplier = table.Column<double>(type: "float", nullable: true),
                    FightPoint_AttackPointBonus = table.Column<int>(type: "int", nullable: true),
                    FightPoint_DefensePointBonus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpireUnits", x => new { x.EmpireId, x.UnitId, x.Level });
                    table.ForeignKey(
                        name: "FK_EmpireUnits_Empires_EmpireId",
                        column: x => x.EmpireId,
                        principalTable: "Empires",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpireUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttackUnits",
                columns: table => new
                {
                    Level = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    AttackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttackUnits", x => new { x.UnitId, x.AttackId, x.Level });
                    table.ForeignKey(
                        name: "FK_AttackUnits_Attacks_AttackId",
                        column: x => x.AttackId,
                        principalTable: "Attacks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttackUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DefenseUnit",
                columns: table => new
                {
                    Level = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    AttackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefenseUnit", x => new { x.UnitId, x.AttackId, x.Level });
                    table.ForeignKey(
                        name: "FK_DefenseUnit_Attacks_AttackId",
                        column: x => x.AttackId,
                        principalTable: "Attacks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DefenseUnit_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpirePlanetUpgrades",
                columns: table => new
                {
                    UpgradeId = table.Column<int>(type: "int", nullable: false),
                    EmpirePlanetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpirePlanetUpgrades", x => new { x.EmpirePlanetId, x.UpgradeId });
                    table.ForeignKey(
                        name: "FK_EmpirePlanetUpgrades_EmpirePlanets_EmpirePlanetId",
                        column: x => x.EmpirePlanetId,
                        principalTable: "EmpirePlanets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpirePlanetUpgrades_Upgrades_UpgradeId",
                        column: x => x.UpgradeId,
                        principalTable: "Upgrades",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "User", "9a42dc18-371b-4255-9b1a-1ec1daa49c54", "User", "USER" },
                    { "Admin", "154e125f-5c2b-43f3-8fd0-c4d715def3dc", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user10", 0, "cfc830af-302f-44b7-a973-805e6439b2ad", null, true, false, null, null, "HFILINKOV9", "AQAAAAEAACcQAAAAEN0cIB8wmen49emtn9Ms5UFXOipcP+6yTvExOBRMxrhSS+uqHnJEapVIh6B49/YIWw==", null, false, 0, "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2", false, "hfilinkov9" },
                    { "user9", 0, "cfc830af-302f-44b7-a973-805e6439b2ad", null, true, false, null, null, "KSEELY8", "AQAAAAEAACcQAAAAEFlJG2ctiddDjJLLD9li2DC6fq+ayTAQKZPaihYxeEt0MN+5BRBACGcpdqhiIH6rxA==", null, false, 0, "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2", false, "kseely8" },
                    { "user8", 0, "cfc830af-302f-44b7-a973-805e6439b2ad", null, true, false, null, null, "ERYLETT7", "AQAAAAEAACcQAAAAEJrpfU5DQURYh/0tm1qja1KrqQM/L6KVN6/3XBNdgrhfeSrgptE1Nz0/x1OdVNacoA==", null, false, 0, "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2", false, "erylett7" },
                    { "user7", 0, "cfc830af-302f-44b7-a973-805e6439b2ad", null, true, false, null, null, "GBOSKELL6", "AQAAAAEAACcQAAAAEC8un/FNYohnJ+QvctjYSlTFfqhlQOFhd2k20bl3pPGrnzAm+ZEKu1H9kKS7nlJK4A==", null, false, 0, "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2", false, "gboskell6" },
                    { "user6", 0, "cfc830af-302f-44b7-a973-805e6439b2ad", null, true, false, null, null, "HCHEVERELL5", "AQAAAAEAACcQAAAAEKYETOHFSC9RX/vSF1WAsekOill/1OksOkBZDmWMC5fKyfBzkpoDM8k9e/KT2S9oLQ==", null, false, 0, "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2", false, "hcheverell5" },
                    { "user5", 0, "cfc830af-302f-44b7-a973-805e6439b2ad", null, true, false, null, null, "TMAXWORTHY4", "AQAAAAEAACcQAAAAEGyVQWtxvOVWPTH4KPq1SkQWkWHIUjetRwFEfSQyKbyfDMbtJb/gIp+f83y7UsA/ug==", null, false, 0, "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2", false, "tmaxworthy4" },
                    { "user4", 0, "cfc830af-302f-44b7-a973-805e6439b2ad", null, true, false, null, null, "JMELIOR3", "AQAAAAEAACcQAAAAEDtpu/pUhVIGWpUKNjeMp3N3l8cuAA+xopYuO6JyXailj1Rc81U9of7Watkz9A37Cw==", null, false, 0, "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2", false, "jmelior3" },
                    { "user3", 0, "cfc830af-302f-44b7-a973-805e6439b2ad", null, true, false, null, null, "BLYPTRATT2", "AQAAAAEAACcQAAAAELefw02fZ4vpR5OmCBTfSqN9tsPt9R1obPspCF6LOLLioh3D4hz/DenQ2+Ih16Dupg==", null, false, 0, "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2", false, "blyptratt2" },
                    { "user2", 0, "cfc830af-302f-44b7-a973-805e6439b2ad", null, true, false, null, null, "LTIPPIN1", "AQAAAAEAACcQAAAAEEzx6zxTlEArYHVBN7EdCQUzJw+seBDxME81/Y2fPS8BhICqZ3hzNm5gIc6zeV4VMA==", null, false, 0, "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2", false, "ltippin1" },
                    { "user1", 0, "cfc830af-302f-44b7-a973-805e6439b2ad", null, true, false, null, null, "SSTRAHAN0", "AQAAAAEAACcQAAAAEM98EQK0Q1fqjApSc6ETzl8G7x8oCe/x9kl8S1Ij/BLCMLJOU6qEH4mBDivwDx/Tcg==", null, false, 0, "RD6YLKPIHDS7MMSLGQ3O7DF5ZNR73XJ2", false, "sstrahan0" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EventType", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "A termelés csökken -15%-kal a rossz termelés miatt.", "event_badharvest", "https://galacticempire.blob.core.windows.net/eventimages/bad_harvest.jpg", "Rossz termés" },
                    { 7, "Katonáid elégedetlenek, ezért 20%-kal kevesebb a támadó és védekező erejük.", "event_unsatisfiedunits", "https://galacticempire.blob.core.windows.net/eventimages/unsatisfied_units.jpg", "Elégedetlen katonák" },
                    { 5, "A lakosság csökken -100 fővel, mert az emberek nem érzik jól magukat a birodalomban.", "event_unsatisfiedpeople", "https://galacticempire.blob.core.windows.net/eventimages/unsatisfied_people.jpg", "Elégedetlen emberek" },
                    { 6, "Katonáid elégedettek, ezért 20%-kal nagyobb a támadó és védekező erejük.", "event_satisfiedunits", "https://galacticempire.blob.core.windows.net/eventimages/satisfied_units.jpg", "Elégedett katonák" },
                    { 4, "A lakosság nő 100 fővel, mert az emberek jól érzik magukat a birodalomban.", "event_satisfiedpeople", "https://galacticempire.blob.core.windows.net/eventimages/satisfied_people.jpg", "Elégedett emberek" },
                    { 2, "A termelés nő 15%-kal a jó termelés miatt.", "event_goodharvest", "https://galacticempire.blob.core.windows.net/eventimages/good_harvest.jpg", "Jó termés" },
                    { 3, "Találtál egy elhagyott rakományt, amivel találtál nyersanyagokat! Mindeből 10000 db jóváírva a birodalomhoz.", "event_jackpot", "https://galacticempire.blob.core.windows.net/eventimages/jackpot.jpg", "Jackpot" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 2, "https://galacticempire.blob.core.windows.net/materialimages/food.png", "Élelem" },
                    { 1, "https://galacticempire.blob.core.windows.net/materialimages/quartz.png", "Kvarc" },
                    { 3, "https://galacticempire.blob.core.windows.net/materialimages/bitcoin.png", "Bitcoin" }
                });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Id", "CapturingTime", "Description", "ImageUrl", "Name", "PlanetType" },
                values: new object[,]
                {
                    { 4, new TimeSpan(0, 0, 5, 0, 0), "Ez egy olyan bolygó, ahol főként csak a bányászok élnek, mert rendkívűl sok kvarc található a felszín alatt.", "https://galacticempire.blob.core.windows.net/planetimages/Darvis.png", "Darvis", "planet_darvis" },
                    { 3, new TimeSpan(0, 0, 5, 0, 0), "Rendkívűli bitcoin bányászat folyik ezen a bolygón, ami kiteszi annak gazdaságát.", "https://galacticempire.blob.core.windows.net/planetimages/Cribatune.png", "Cribatune", "planet_cribatune" },
                    { 2, new TimeSpan(0, 0, 5, 0, 0), "Egy bolygó, amin nagyon sok ember él és elég jól áll nyersanyaggal. Viszont azt mondják lakik erre egy őrült tudós...", "https://galacticempire.blob.core.windows.net/planetimages/C137_Earth.png", "Föld C-137", "planet_c137earth" },
                    { 10, new TimeSpan(0, 0, 5, 0, 0), "A kozmikus háborúk miatt néhány bolygón továbbra is felkészültek a következő háborúra. Ezen a bolygón főleg hadegységek szoktak szállásolni.", "https://galacticempire.blob.core.windows.net/planetimages/Yoiphus.png", "Yoiphus", "planet_yoiphus" },
                    { 9, new TimeSpan(0, 0, 5, 0, 0), "A 224. galaktikus űrcsatában vesztes oldalon állt, aminek következtében elszegényedett. Vedd be a birodalomba és lendítsd fel a gazdaságát!", "https://galacticempire.blob.core.windows.net/planetimages/Sidatania.png", "Sidatania", "planet_sidatania" },
                    { 8, new TimeSpan(0, 0, 5, 0, 0), "Kevés népességszámához képest egy rendkívűl gazdag bolygóról beszélünk. Rengeteg kvarcot és bitcoint bányásznak.", "https://galacticempire.blob.core.windows.net/planetimages/Nusobos.png", "Nusobos", "planet_nusobos" },
                    { 7, new TimeSpan(0, 0, 5, 0, 0), "Kereskedőbolygó, aminek a kvarc a fő export cikke. A kvarc ami gazdaggá tette ezt a bolygót.", "https://galacticempire.blob.core.windows.net/planetimages/Heolara.png", "Heolara", "planet_heolara" },
                    { 6, new TimeSpan(0, 0, 5, 0, 0), "Egy átlagos bolygó az X-12Z Naprendszerben. Vajon tényleg átlagos?", "https://galacticempire.blob.core.windows.net/planetimages/Gingeria.png", "Gingeria", "planet_gingeria" },
                    { 5, new TimeSpan(0, 0, 5, 0, 0), "Ez egy teljesen egyszerű bolygó, ami mindent kitermel magának és nincs szüksége export cikkek vételére.", "https://galacticempire.blob.core.windows.net/planetimages/Dillon.png", "Dillon", "planet_dillon" },
                    { 11, new TimeSpan(0, 0, 5, 0, 0), "Nem egy nagy bolygó, de bitcoinban gazdagodik rendesen. Itt lehet kapni RX 3080 videókártyát az biztos.", "https://galacticempire.blob.core.windows.net/planetimages/Zuccars.png", "Zuccars", "planet_zuccars" },
                    { 1, new TimeSpan(0, 0, 5, 0, 0), "Főleg katonai létesítményeket üzemeltetnek a bolygón. Ha háborúzni szeretnél, ezt a bolygót csatold a birodalmadba!", "https://galacticempire.blob.core.windows.net/planetimages/Avypso.png", "Avypso", "planet_avypso" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "ImageUrl", "MercenaryPerHour", "Name", "RankPoint", "SupplyPerHour" },
                values: new object[,]
                {
                    { 2, "https://galacticempire.blob.core.windows.net/unitimages/space_cruiser.png", 3, "Űrcirkáló", 3, 6 },
                    { 5, "https://galacticempire.blob.core.windows.net/unitimages/scout_drone.png", 2, "Felderítő drón", 1, 0 },
                    { 4, "https://galacticempire.blob.core.windows.net/unitimages/millenium_falcon.png", 8, "Ezeréves sólyom", 8, 15 },
                    { 3, "https://galacticempire.blob.core.windows.net/unitimages/ironman.png", 5, "Vasember", 5, 8 },
                    { 1, "https://galacticempire.blob.core.windows.net/unitimages/solar_sail.png", 2, "Napvitorlás", 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "Upgrades",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "UpgradeTime", "UpgradeType" },
                values: new object[,]
                {
                    { 7, "A birodalomban lévő egységek támadása és védelme 20%-kal nő.", "https://galacticempire.blob.core.windows.net/upgradeimages/vibranium_shield.jpg", "Vibránium páncél", new TimeSpan(0, 0, 1, 0, 0), "upgrade_vibraniumarmor" },
                    { 6, "A birodalom maximális egységszáma 10000 fővel nő.", "https://galacticempire.blob.core.windows.net/upgradeimages/secret_military_base.png", "Titkos katonai bázis", new TimeSpan(0, 0, 1, 0, 0), "upgrade_secretmilitarybase" },
                    { 5, "A bolygó további 20%-kal több kvarcot termel a birodalom számára.", "https://galacticempire.blob.core.windows.net/upgradeimages/quartz_mine.jpg", "Kvarcbánya", new TimeSpan(0, 0, 1, 0, 0), "upgrade_quartzmine" },
                    { 3, "A birodalomban lévő egységek védelme 30%-kal nő.", "https://galacticempire.blob.core.windows.net/upgradeimages/kinetic_shield.png", "Kinetikus pajzs", new TimeSpan(0, 0, 1, 0, 0), "upgrade_kineticshield" }
                });

            migrationBuilder.InsertData(
                table: "Upgrades",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "UpgradeTime", "UpgradeType" },
                values: new object[,]
                {
                    { 2, "A bolygó további 20%-kal több ételt termel a birodalom számára.", "https://galacticempire.blob.core.windows.net/upgradeimages/interdimensional_garden.jpg", "Interdimenzionális gasztrokert", new TimeSpan(0, 0, 1, 0, 0), "upgrade_interdimensionalgastrogarden" },
                    { 1, "A birodalomban lévő maximális populáció emelkedik 1000000 fővel és az új lakóhelyeknek köszönhetően a jelenlegi populáció 20%-kal nő.", "https://galacticempire.blob.core.windows.net/upgradeimages/futuristic_residential_area.jpg", "Futurisztikus lakónegyed", new TimeSpan(0, 0, 1, 0, 0), "upgrade_futuristicresidentialarea" },
                    { 8, "A bolygó további 20%-kal több bitcoint termel a birodalom számára.", "https://galacticempire.blob.core.windows.net/upgradeimages/videocard_expansion.jpg", "Videókártya bővítés", new TimeSpan(0, 0, 1, 0, 0), "upgrade_videocardexpension" },
                    { 4, "A birodalomban lévő egységek támadása 30%-kal nő.", "https://galacticempire.blob.core.windows.net/upgradeimages/laser_weapon.jpg", "Lézerfegyverek", new TimeSpan(0, 0, 1, 0, 0), "upgrade_laserweapon" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "User", "user10" },
                    { "Admin", "user1" },
                    { "User", "user2" },
                    { "User", "user3" },
                    { "User", "user4" },
                    { "User", "user9" },
                    { "User", "user6" },
                    { "User", "user7" },
                    { "User", "user8" },
                    { "User", "user5" }
                });

            migrationBuilder.InsertData(
                table: "Empires",
                columns: new[] { "Id", "MaxNumberOfPopulation", "MaxNumberOfUnits", "Name", "OwnerId", "Population" },
                values: new object[,]
                {
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 1000000, 100, "Center", "user1", 1000 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 1000000, 100, "Londonderry", "user9", 1000 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 1000000, 100, "Kipling", "user8", 1000 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 1000000, 100, "Melody", "user7", 1000 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 1000000, 100, "Norway Maple", "user6", 1000 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 1000000, 100, "Carioca", "user5", 1000 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 1000000, 100, "Algoma", "user4", 1000 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 1000000, 100, "Gale", "user3", 1000 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 1000000, 100, "Melrose", "user2", 1000 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 1000000, 100, "Arkansas", "user10", 1000 }
                });

            migrationBuilder.InsertData(
                table: "PlanetPriceMaterials",
                columns: new[] { "MaterialId", "PlanetId", "Amount" },
                values: new object[,]
                {
                    { 3, 1, 10000 },
                    { 3, 7, 10000 },
                    { 3, 8, 10000 },
                    { 3, 9, 10000 },
                    { 3, 10, 10000 },
                    { 3, 6, 10000 },
                    { 3, 2, 10000 },
                    { 3, 11, 10000 },
                    { 3, 5, 10000 },
                    { 3, 4, 10000 },
                    { 3, 3, 10000 }
                });

            migrationBuilder.InsertData(
                table: "PlanetProperties",
                columns: new[] { "PlanetId", "BaseBitcoin", "BaseFood", "BaseQuartz", "MaxPopulationCount", "MaxUnitCount" },
                values: new object[,]
                {
                    { 8, 160, 70, 140, 85000, 60 },
                    { 9, 100, 80, 80, 500000, 50 },
                    { 6, 120, 60, 80, 700000, 120 },
                    { 5, 100, 100, 100, 100000, 100 },
                    { 4, 30, 20, 180, 50000, 10 },
                    { 3, 150, 40, 75, 240000, 110 },
                    { 2, 60, 100, 120, 1000000, 100 },
                    { 11, 200, 85, 50, 305000, 20 },
                    { 10, 50, 140, 70, 450000, 240 },
                    { 1, 20, 140, 70, 100000, 200 },
                    { 7, 80, 40, 170, 250000, 140 }
                });

            migrationBuilder.InsertData(
                table: "UnitLevels",
                columns: new[] { "Level", "UnitId", "AttackPoint", "DefensePoint", "TrainingTime" },
                values: new object[,]
                {
                    { 1, 1, 2, 6, new TimeSpan(0, 0, 0, 15, 0) },
                    { 2, 1, 3, 9, new TimeSpan(0, 0, 0, 22, 0) },
                    { 3, 1, 4, 12, new TimeSpan(0, 0, 0, 30, 0) },
                    { 1, 2, 4, 6, new TimeSpan(0, 0, 0, 20, 0) },
                    { 2, 2, 6, 9, new TimeSpan(0, 0, 0, 30, 0) },
                    { 3, 2, 8, 12, new TimeSpan(0, 0, 0, 40, 0) },
                    { 2, 3, 10, 10, new TimeSpan(0, 0, 0, 37, 0) },
                    { 3, 3, 14, 14, new TimeSpan(0, 0, 0, 50, 0) },
                    { 1, 4, 10, 12, new TimeSpan(0, 0, 0, 30, 0) },
                    { 2, 4, 15, 18, new TimeSpan(0, 0, 0, 45, 0) },
                    { 3, 4, 20, 24, new TimeSpan(0, 0, 1, 0, 0) },
                    { 1, 3, 7, 7, new TimeSpan(0, 0, 0, 25, 0) },
                    { 1, 5, 0, 0, new TimeSpan(0, 0, 0, 10, 0) }
                });

            migrationBuilder.InsertData(
                table: "UnitPriceMaterials",
                columns: new[] { "MaterialId", "UnitId", "Amount" },
                values: new object[,]
                {
                    { 3, 3, 300 },
                    { 3, 5, 100 },
                    { 3, 4, 420 },
                    { 3, 2, 140 },
                    { 3, 1, 100 }
                });

            migrationBuilder.InsertData(
                table: "UpgradePriceMaterials",
                columns: new[] { "MaterialId", "UpgradeId", "Amount" },
                values: new object[,]
                {
                    { 1, 7, 10000 },
                    { 1, 6, 10000 },
                    { 1, 4, 10000 },
                    { 1, 3, 10000 },
                    { 1, 2, 10000 },
                    { 1, 1, 10000 },
                    { 1, 8, 10000 },
                    { 1, 5, 10000 }
                });

            migrationBuilder.InsertData(
                table: "EmpireMaterials",
                columns: new[] { "EmpireId", "MaterialId", "Amount", "BaseProduction", "ProductionMultiplier" },
                values: new object[,]
                {
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 1, 10000, 10.0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 2, 10000, 10.0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 1, 10000, 10.0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 2, 10000, 10.0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 1, 10000, 10.0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 2, 10000, 10.0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 3, 10000, 10.0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 1, 10000, 10.0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 3, 10000, 10.0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 1, 10000, 10.0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 3, 10000, 10.0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 1, 10000, 10.0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 3, 10000, 10.0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 3, 10000, 10.0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 1, 10000, 10.0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 2, 10000, 10.0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 3, 10000, 10.0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 2, 10000, 10.0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 3, 10000, 10.0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 2, 10000, 10.0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 1, 10000, 10.0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 3, 10000, 10.0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 2, 10000, 10.0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 2, 10000, 10.0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 1, 10000, 10.0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 3, 10000, 10.0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 2, 10000, 10.0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 1, 10000, 10.0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 2, 10000, 10.0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 3, 10000, 10.0, 1.0 }
                });

            migrationBuilder.InsertData(
                table: "EmpireUnits",
                columns: new[] { "EmpireId", "Level", "UnitId", "Amount", "FightPoint_AttackPointBonus", "FightPoint_AttackPointMultiplier", "FightPoint_DefensePointBonus", "FightPoint_DefensePointMultiplier" },
                values: new object[,]
                {
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 3, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 1, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 1, 5, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 1, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 2, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 2, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 2, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 2, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 1, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 3, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 1, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 3, 1, 0, 0, 1.0, 0, 1.0 }
                });

            migrationBuilder.InsertData(
                table: "EmpireUnits",
                columns: new[] { "EmpireId", "Level", "UnitId", "Amount", "FightPoint_AttackPointBonus", "FightPoint_AttackPointMultiplier", "FightPoint_DefensePointBonus", "FightPoint_DefensePointMultiplier" },
                values: new object[,]
                {
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 2, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 1, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 1, 5, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 3, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 2, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 1, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 3, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 2, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("bf37d8cc-0744-4054-1111-603e6829799a"), 3, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 3, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 3, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 2, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 2, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 3, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 1, 5, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 1, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 2, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 3, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 1, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 1, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 3, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 1, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 2, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 3, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 1, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 2, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 2, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 1, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 3, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 1, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 3, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 1, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 2, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 1, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 1, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 2, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 2, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 3, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 1, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 2, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 3, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 1, 2, 0, 0, 1.0, 0, 1.0 }
                });

            migrationBuilder.InsertData(
                table: "EmpireUnits",
                columns: new[] { "EmpireId", "Level", "UnitId", "Amount", "FightPoint_AttackPointBonus", "FightPoint_AttackPointMultiplier", "FightPoint_DefensePointBonus", "FightPoint_DefensePointMultiplier" },
                values: new object[,]
                {
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 2, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("0b62f843-4357-423b-1111-a2506ac91d5c"), 3, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("488d40fe-e2c5-41e3-1111-dea16b7c2897"), 1, 5, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 3, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 1, 5, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 1, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 3, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 1, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 2, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 3, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 1, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 2, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 2, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 3, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 1, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 2, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 3, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 1, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 2, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 3, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 1, 5, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 1, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 1, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 2, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 1, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 2, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 3, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 1, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 2, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 3, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 3, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 1, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 3, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 1, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 2, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 3, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 1, 5, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("72ff37e8-5888-47c6-1111-15844a6449b1"), 1, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("af378505-14cb-4f49-1111-ba2c8fdef77d"), 2, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 2, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 2, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 1, 4, 0, 0, 1.0, 0, 1.0 }
                });

            migrationBuilder.InsertData(
                table: "EmpireUnits",
                columns: new[] { "EmpireId", "Level", "UnitId", "Amount", "FightPoint_AttackPointBonus", "FightPoint_AttackPointMultiplier", "FightPoint_DefensePointBonus", "FightPoint_DefensePointMultiplier" },
                values: new object[,]
                {
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 3, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 1, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 2, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 3, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 1, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 2, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 2, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 3, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 2, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 3, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 3, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 1, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 2, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("392a9574-11a7-4f01-1111-4980933cc7a6"), 3, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 1, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 3, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("cbbd70fb-06cd-4368-1111-93c237980d8c"), 1, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 3, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 2, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 3, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("a63a97aa-4ae8-4185-1111-be02286b1542"), 1, 5, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 1, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 2, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 3, 1, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 1, 5, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 1, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 3, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 1, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 2, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 3, 3, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 1, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 2, 4, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c4393fff-8d3a-4508-1111-794916e9e997"), 2, 2, 0, 0, 1.0, 0, 1.0 },
                    { new Guid("c0b59d8d-58cc-4a54-a045-bf2a9341c658"), 1, 5, 0, 0, 1.0, 0, 1.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllianceInvitations_InvitedEmpireId",
                table: "AllianceInvitations",
                column: "InvitedEmpireId");

            migrationBuilder.CreateIndex(
                name: "IX_AllianceInvitations_InviterEmpireId",
                table: "AllianceInvitations",
                column: "InviterEmpireId");

            migrationBuilder.CreateIndex(
                name: "IX_AllianceMembers_EmpireId",
                table: "AllianceMembers",
                column: "EmpireId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_AttackerId",
                table: "Attacks",
                column: "AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_DefenderId",
                table: "Attacks",
                column: "DefenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AttackUnits_AttackId",
                table: "AttackUnits",
                column: "AttackId");

            migrationBuilder.CreateIndex(
                name: "IX_DefenseUnit_AttackId",
                table: "DefenseUnit",
                column: "AttackId");

            migrationBuilder.CreateIndex(
                name: "IX_DroneAttacks_AttackerId",
                table: "DroneAttacks",
                column: "AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_DroneAttacks_DefenderId",
                table: "DroneAttacks",
                column: "DefenderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpireEvents_EmpireId",
                table: "EmpireEvents",
                column: "EmpireId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpireEvents_EventId",
                table: "EmpireEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpireMaterials_MaterialId",
                table: "EmpireMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpirePlanets_EmpireId",
                table: "EmpirePlanets",
                column: "EmpireId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpirePlanets_PlanetId",
                table: "EmpirePlanets",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpirePlanetUpgrades_UpgradeId",
                table: "EmpirePlanetUpgrades",
                column: "UpgradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Empires_OwnerId",
                table: "Empires",
                column: "OwnerId",
                unique: true,
                filter: "[OwnerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmpireUnits_UnitId",
                table: "EmpireUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanetPriceMaterials_MaterialId",
                table: "PlanetPriceMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceMaterials_MaterialId",
                table: "UnitPriceMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_UpgradePriceMaterials_MaterialId",
                table: "UpgradePriceMaterials",
                column: "MaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveAttackings");

            migrationBuilder.DropTable(
                name: "ActiveCapturings");

            migrationBuilder.DropTable(
                name: "ActiveSpyings");

            migrationBuilder.DropTable(
                name: "ActiveTrainings");

            migrationBuilder.DropTable(
                name: "ActiveUpgradings");

            migrationBuilder.DropTable(
                name: "AllianceInvitations");

            migrationBuilder.DropTable(
                name: "AllianceMembers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AttackUnits");

            migrationBuilder.DropTable(
                name: "DefenseUnit");

            migrationBuilder.DropTable(
                name: "DroneAttacks");

            migrationBuilder.DropTable(
                name: "EmpireEvents");

            migrationBuilder.DropTable(
                name: "EmpireMaterials");

            migrationBuilder.DropTable(
                name: "EmpirePlanetUpgrades");

            migrationBuilder.DropTable(
                name: "EmpireUnits");

            migrationBuilder.DropTable(
                name: "PlanetPriceMaterials");

            migrationBuilder.DropTable(
                name: "PlanetProperties");

            migrationBuilder.DropTable(
                name: "UnitLevels");

            migrationBuilder.DropTable(
                name: "UnitPriceMaterials");

            migrationBuilder.DropTable(
                name: "UpgradePriceMaterials");

            migrationBuilder.DropTable(
                name: "Alliances");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Attacks");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EmpirePlanets");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Upgrades");

            migrationBuilder.DropTable(
                name: "Empires");

            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
