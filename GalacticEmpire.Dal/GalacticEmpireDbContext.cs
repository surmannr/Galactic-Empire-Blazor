using GalacticEmpire.Dal.EntityConfigurations.Empire;
using GalacticEmpire.Dal.EntityConfigurations.Event;
using GalacticEmpire.Dal.EntityConfigurations.Material;
using GalacticEmpire.Dal.EntityConfigurations.Planet;
using GalacticEmpire.Dal.EntityConfigurations.Unit;
using GalacticEmpire.Dal.EntityConfigurations.Upgrade;
using GalacticEmpire.Dal.EntityConfigurations.User;
using GalacticEmpire.Dal.EntityConfigurations.UserConfig;
using GalacticEmpire.Domain.Models.AllianceModel;
using GalacticEmpire.Domain.Models.AllianceModel.Base;
using GalacticEmpire.Domain.Models.EventModel;
using GalacticEmpire.Domain.Models.EventModel.Base;
using GalacticEmpire.Domain.Models.PlanetModel.Base;
using GalacticEmpire.Domain.Models.PlanetModel.Type;
using GalacticEmpire.Domain.Models.UpgradeModel.Base;
using GalacticEmpire.Domain.Models.UpgradeModel.Types;
using GalacticEmpire.Domain.Models.UserModel.Base;
using GalacticEmpire.Shared.Constants.Event;
using GalacticEmpire.Shared.Constants.Planet;
using GalacticEmpire.Shared.Constants.Upgrade;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace GalacticEmpire.Dal
{
    public class GalacticEmpireDbContext : ApiAuthorizationDbContext<User>
    {
        public DbSet<Alliance> Alliances { get; set; }
        public DbSet<AllianceInvitation> AllianceInvitations { get; set; }
        
        public GalacticEmpireDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Alliance>()
                .AllianceConnection();

            builder.Entity<AllianceInvitation>()
                .AllianceInvitationConnection();

            builder.Entity<Upgrade>()
                .UpgradeInheritance();

            builder.Entity<Planet>()
                .PlanetInheritance();

            builder.Entity<Event>()
                .EventInheritance();

            builder.ApplyConfigurationsInOrder();
        }
        
    }

    internal static class DbExtension
    {
        // Connections
        public static EntityTypeBuilder<Alliance> AllianceConnection(this EntityTypeBuilder<Alliance> builder)
        {
            builder.HasOne(a => a.LeaderEmpire)
                .WithOne(a => a.OwnedAlliance)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Members)
                .WithOne(a => a.Alliance)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.AllianceInvitations)
                .WithOne(a => a.Alliance)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<AllianceInvitation> AllianceInvitationConnection(this EntityTypeBuilder<AllianceInvitation> builder)
        {


            return builder;
        }

        // Inheritance
        public static EntityTypeBuilder<Upgrade> UpgradeInheritance(this EntityTypeBuilder<Upgrade> builder)
        {
            builder.HasDiscriminator(x => x.UpgradeType)
                .HasValue<Upgrade>(UpgradeTypeConstants.Base)
                .HasValue<FuturisticResidentialAreaUpgrade>(UpgradeTypeConstants.FuturisticResidentialAreaType)
                .HasValue<InterdimensionalGastrogardenUpgrade>(UpgradeTypeConstants.InterdimensionalGastrogardenType)
                .HasValue<KineticShieldUpgrade>(UpgradeTypeConstants.KineticShieldType)
                .HasValue<LaserWeaponsUpgrade>(UpgradeTypeConstants.LaserWeaponsType)
                .HasValue<QuartzMineUpgrade>(UpgradeTypeConstants.QuartzMineType)
                .HasValue<SecretMilitaryBaseUpgrade>(UpgradeTypeConstants.SecretMilitaryBaseType)
                .HasValue<VibraniumArmorUpgrade>(UpgradeTypeConstants.VibraniumArmorType)
                .HasValue<VideocardExpansionUpgrade>(UpgradeTypeConstants.VideocardExpansionType);

            return builder;
        }

        public static EntityTypeBuilder<Event> EventInheritance(this EntityTypeBuilder<Event> builder)
        {
            builder.HasDiscriminator(x => x.EventType)
                .HasValue<Event>(EventTypeConstants.Base)
                .HasValue<BadHarvestEvent>(EventTypeConstants.BadHarvestType)
                .HasValue<GoodHarvestEvent>(EventTypeConstants.GoodHarvestType)
                .HasValue<JackpotEvent>(EventTypeConstants.JackpotType)
                .HasValue<SatisfiedPeopleEvent>(EventTypeConstants.SatisfiedPeopleType)
                .HasValue<UnsatisfiedPeopleEvent>(EventTypeConstants.UnsatisfiedPeopleType)
                .HasValue<SatisfiedUnitsEvent>(EventTypeConstants.SatisfiedUnitsType)
                .HasValue<UnsatisfiedUnitsEvent>(EventTypeConstants.UnsatisfiedUnitsType);

            return builder;
        }

        public static EntityTypeBuilder<Planet> PlanetInheritance(this EntityTypeBuilder<Planet> builder)
        {
            builder.HasDiscriminator(x => x.PlanetType)
                .HasValue<Planet>(PlanetTypeConstants.Base)
                .HasValue<AvypsoPlanet>(PlanetTypeConstants.AvypsoType)
                .HasValue<C137EarthPlanet>(PlanetTypeConstants.C137EarthType)
                .HasValue<CribatunePlanet>(PlanetTypeConstants.CribatuneType)
                .HasValue<DarvisPlanet>(PlanetTypeConstants.DarvisType)
                .HasValue<DillonPlanet>(PlanetTypeConstants.DillonType)
                .HasValue<GingeriaPlanet>(PlanetTypeConstants.GingeriaType)
                .HasValue<HeolaraPlanet>(PlanetTypeConstants.HeolaraType)
                .HasValue<NusobosPlanet>(PlanetTypeConstants.NusobosType)
                .HasValue<SidataniaPlanet>(PlanetTypeConstants.SidataniaType)
                .HasValue<YoiphusPlanet>(PlanetTypeConstants.YoiphusType)
                .HasValue<ZuccarsPlanet>(PlanetTypeConstants.ZuccarsType);

            return builder;
        }

        // Configurations
        public static void ApplyConfigurationsInOrder(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MaterialEntityConfiguration());

            builder.ApplyConfiguration(new EventEntityConfiguration());

            builder.ApplyConfiguration(new PlanetEntityConfiguration());
            builder.ApplyConfiguration(new PlanetPriceMaterialEntityConfiguration());

            builder.ApplyConfiguration(new UpgradeEntityConfiguration());
            builder.ApplyConfiguration(new UpgradePriceMaterialEntityConfiguration());

            builder.ApplyConfiguration(new UserEntityConfiguration());
            builder.ApplyConfiguration(new RoleEntityConfiguration());
            builder.ApplyConfiguration(new UserRoleEntityConfiguration());

            builder.ApplyConfiguration(new UnitEntityConfiguration());
            builder.ApplyConfiguration(new UnitLevelEntityConfiguration());
            builder.ApplyConfiguration(new UnitPriceMaterialEntityConfiguration());

            builder.ApplyConfiguration(new EmpireEntityConfiguration());
            builder.ApplyConfiguration(new EmpireMaterialEntityConfiguration());
        }
    }
}
