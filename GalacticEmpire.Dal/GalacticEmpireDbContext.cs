using GalacticEmpire.Dal.EntityConfigurations.Empire;
using GalacticEmpire.Dal.EntityConfigurations.Material;
using GalacticEmpire.Dal.EntityConfigurations.Planet;
using GalacticEmpire.Dal.EntityConfigurations.Unit;
using GalacticEmpire.Dal.EntityConfigurations.Upgrade;
using GalacticEmpire.Dal.EntityConfigurations.User;
using GalacticEmpire.Dal.EntityConfigurations.UserConfig;
using GalacticEmpire.Domain.Models.AllianceModel;
using GalacticEmpire.Domain.Models.AllianceModel.Base;
using GalacticEmpire.Domain.Models.AttackModel;
using GalacticEmpire.Domain.Models.AttackModel.Base;
using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.EventModel;
using GalacticEmpire.Domain.Models.EventModel.Base;
using GalacticEmpire.Domain.Models.MaterialModel.Base;
using GalacticEmpire.Domain.Models.PlanetModel;
using GalacticEmpire.Domain.Models.PlanetModel.Base;
using GalacticEmpire.Domain.Models.PlanetModel.Type;
using GalacticEmpire.Domain.Models.UnitModel;
using GalacticEmpire.Domain.Models.UnitModel.Base;
using GalacticEmpire.Domain.Models.UpgradeModel;
using GalacticEmpire.Domain.Models.UpgradeModel.Base;
using GalacticEmpire.Domain.Models.UpgradeModel.Types;
using GalacticEmpire.Domain.Models.UserModel.Base;
using GalacticEmpire.Shared.Constants.Event;
using GalacticEmpire.Shared.Constants.Planet;
using GalacticEmpire.Shared.Constants.Upgrade;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace GalacticEmpire.Dal
{
    public class GalacticEmpireDbContext : IdentityDbContext<User>
    {
        public GalacticEmpireDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Alliance> Alliances { get; set; }
        public DbSet<AllianceInvitation> AllianceInvitations { get; set; }
        public DbSet<AllianceMember> AllianceMembers { get; set; }

        public DbSet<Attack> Attacks { get; set; }
        public DbSet<DroneAttack> DroneAttacks { get; set; }
        public DbSet<AttackUnit> AttackUnits { get; set; }

        public DbSet<Empire> Empires { get; set; }
        public DbSet<EmpireEvent> EmpireEvents { get; set; }
        public DbSet<EmpireMaterial> EmpireMaterials { get; set; }
        public DbSet<EmpirePlanet> EmpirePlanets { get; set; }
        public DbSet<EmpirePlanetUpgrade> EmpirePlanetUpgrades { get; set; }
        public DbSet<EmpireUnit> EmpireUnits { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<Planet> Planets { get; set; }
        public DbSet<PlanetProperty> PlanetProperties { get; set; }
        public DbSet<PlanetPriceMaterial> PlanetPriceMaterials { get; set; }

        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitLevel> UnitLevels { get; set; }
        public DbSet<UnitPriceMaterial> UnitPriceMaterials { get; set; }

        public DbSet<Upgrade> Upgrades { get; set; }
        public DbSet<UpgradePriceMaterial> UpgradePriceMaterials { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Alliance>()
                .AllianceConnection();

            builder.Entity<AllianceInvitation>()
                .AllianceInvitationConnection();

            builder.Entity<AllianceMember>()
                .AllianceMemberConnection();

            builder.Entity<Attack>()
                .AttackConnection();

            builder.Entity<DroneAttack>()
                .DroneAttackConnection();

            builder.Entity<AttackUnit>()
                .AttackUnitConnection();

            builder.Entity<Empire>()
                .EmpireConnection();

            builder.Entity<EmpireEvent>()
                .EmpireEventConnection();

            builder.Entity<EmpireMaterial>()
                .EmpireMaterialConnection();

            builder.Entity<EmpirePlanet>()
                .EmpirePlanetConnection();

            builder.Entity<EmpirePlanetUpgrade>()
                .EmpirePlanetUpgradeConnection();

            builder.Entity<EmpireUnit>()
                .EmpireUnitConnection();

            builder.Entity<Event>()
                .EventConnection()
                .EventInheritance();

            builder.Entity<Material>()
                .MaterialConnection();

            builder.Entity<Planet>()
                .PlanetConnection()
                .PlanetInheritance();

            builder.Entity<PlanetProperty>()
                .PlanetPropertyConnection();

            builder.Entity<PlanetPriceMaterial>()
                .PlanetPriceMaterialConnection();

            builder.Entity<Unit>()
                .UnitConnection();

            builder.Entity<UnitLevel>()
                .UnitLevelConnection();

            builder.Entity<UnitPriceMaterial>()
                .UnitPriceMaterialConnection();

            builder.Entity<Upgrade>()
                .UpgradeConnection()
                .UpgradeInheritance();

            builder.Entity<UpgradePriceMaterial>()
                .UpgradePriceMaterialConnection();

            builder.Entity<User>()
                .UserConnection();

            builder.ApplyConfigurationsInOrder();
        }
        
    }

    internal static class DbExtension
    {
        // Connections
        public static EntityTypeBuilder<Alliance> AllianceConnection(this EntityTypeBuilder<Alliance> builder)
        {
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
            builder.HasKey(e => new { e.AllianceId, e.InvitedEmpireId, e.InviterEmpireId });

            builder.HasOne(a => a.Alliance)
                .WithMany(a => a.AllianceInvitations)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.InviterEmpire)
                .WithMany(a => a.AllianceSentInvitations)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.InvitedEmpire)
                .WithMany(a => a.AllianceReceivedInvitations)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<AllianceMember> AllianceMemberConnection(this EntityTypeBuilder<AllianceMember> builder)
        {
            builder.HasKey(e => new { e.AllianceId, e.EmpireId });

            builder.HasOne(a => a.Alliance)
                .WithMany(a => a.Members)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Empire)
                .WithOne(a => a.Alliance)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.InvitationRight)
                .HasDefaultValue(false);

            return builder;
        }

        public static EntityTypeBuilder<Attack> AttackConnection(this EntityTypeBuilder<Attack> builder)
        {
            builder.HasMany(e => e.AttackUnits)
                .WithOne(e => e.Attack)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Attacker)
                .WithMany(e => e.AttackerAttack)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Defender)
                .WithMany(e => e.DefenderAttack)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<DroneAttack> DroneAttackConnection(this EntityTypeBuilder<DroneAttack> builder)
        {
            builder.HasOne(e => e.Attacker)
                .WithMany(e => e.DroneAttackerAttack)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Defender)
                .WithMany(e => e.DroneDefenderAttack)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<AttackUnit> AttackUnitConnection(this EntityTypeBuilder<AttackUnit> builder)
        {
            builder.HasKey(e => new { e.UnitId, e.AttackId, e.Level });

            builder.HasOne(e => e.Unit)
                .WithMany(e => e.AttackUnits)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Attack)
                .WithMany(e => e.AttackUnits)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<Empire> EmpireConnection(this EntityTypeBuilder<Empire> builder)
        {
            builder.HasOne(e => e.Owner)
                .WithOne(e => e.Empire)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Alliance)
                .WithOne(e => e.Empire)
                .HasForeignKey<AllianceMember>(e => e.EmpireId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.EmpireEvents)
                .WithOne(e => e.Empire)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.EmpireMaterials)
                .WithOne(e => e.Empire)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.EmpirePlanets)
                .WithOne(e => e.Empire)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.EmpireUnits)
                .WithOne(e => e.Empire)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.AttackerAttack)
                .WithOne(e => e.Attacker)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.DefenderAttack)
                .WithOne(e => e.Defender)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.DroneAttackerAttack)
                .WithOne(e => e.Attacker)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.DroneDefenderAttack)
                .WithOne(e => e.Defender)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<EmpireEvent> EmpireEventConnection(this EntityTypeBuilder<EmpireEvent> builder)
        {
            builder.HasOne(e => e.Empire)
                .WithMany(e => e.EmpireEvents)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Event)
                .WithMany(e => e.EmpireEvents)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<EmpireMaterial> EmpireMaterialConnection(this EntityTypeBuilder<EmpireMaterial> builder)
        {
            builder.HasKey(e => new { e.EmpireId, e.MaterialId });

            builder.HasOne(e => e.Empire)
                .WithMany(e => e.EmpireMaterials)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Material)
                .WithMany(e => e.EmpireMaterials)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<EmpirePlanet> EmpirePlanetConnection(this EntityTypeBuilder<EmpirePlanet> builder)
        {
            builder.HasOne(e => e.Empire)
                .WithMany(e => e.EmpirePlanets)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Planet)
                .WithMany(e => e.EmpirePlanets)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.EmpirePlanetUpgrades)
                .WithOne(e => e.EmpirePlanet)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<EmpirePlanetUpgrade> EmpirePlanetUpgradeConnection(this EntityTypeBuilder<EmpirePlanetUpgrade> builder)
        {
            builder.HasKey(e => new { e.EmpirePlanetId, e.UpgradeId });

            builder.HasOne(e => e.Upgrade)
                .WithMany(e => e.PlanetUpgrades)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.EmpirePlanet)
                .WithMany(e => e.EmpirePlanetUpgrades)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<EmpireUnit> EmpireUnitConnection(this EntityTypeBuilder<EmpireUnit> builder)
        {
            builder.HasKey(e => new { e.EmpireId, e.UnitId, e.Level });

            builder.HasOne(e => e.Unit)
                .WithMany(e => e.EmpireUnits)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Empire)
                .WithMany(e => e.EmpireUnits)
                .OnDelete(DeleteBehavior.NoAction);

            builder.OwnsOne(e => e.FightPoint);

            return builder;
        }

        public static EntityTypeBuilder<Event> EventConnection(this EntityTypeBuilder<Event> builder)
        {
            builder.HasMany(e => e.EmpireEvents)
                .WithOne(e => e.Event)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<Material> MaterialConnection(this EntityTypeBuilder<Material> builder)
        {
            builder.HasMany(e => e.UnitPriceMaterials)
                .WithOne(e => e.Material)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.PlanetPriceMaterials)
                .WithOne(e => e.Material)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.UpgradePriceMaterials)
                .WithOne(e => e.Material)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.EmpireMaterials)
                .WithOne(e => e.Material)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<Planet> PlanetConnection(this EntityTypeBuilder<Planet> builder)
        {
            builder.HasMany(e => e.EmpirePlanets)
                .WithOne(e => e.Planet)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.PlanetPriceMaterials)
                .WithOne(e => e.Planet)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.PlanetProperty)
                .WithOne(e => e.Planet)
                .HasForeignKey<PlanetProperty>(e => e.PlanetId);

            return builder;
        }

        public static EntityTypeBuilder<PlanetProperty> PlanetPropertyConnection(this EntityTypeBuilder<PlanetProperty> builder)
        {
            builder.HasKey(e => e.PlanetId);

            builder.HasOne(e => e.Planet)
                .WithOne(e => e.PlanetProperty);

            return builder;
        }

        public static EntityTypeBuilder<PlanetPriceMaterial> PlanetPriceMaterialConnection(this EntityTypeBuilder<PlanetPriceMaterial> builder)
        {
            builder.HasKey(e => new { e.PlanetId, e.MaterialId });

            builder.HasOne(e => e.Material)
                .WithMany(e => e.PlanetPriceMaterials)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Planet)
                .WithMany(e => e.PlanetPriceMaterials)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<Unit> UnitConnection(this EntityTypeBuilder<Unit> builder)
        {
            builder.HasMany(e => e.UnitLevels)
                .WithOne(e => e.Unit)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.EmpireUnits)
                .WithOne(e => e.Unit)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.UnitPriceMaterials)
                .WithOne(e => e.Unit)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.AttackUnits)
                .WithOne(e => e.Unit)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<UnitLevel> UnitLevelConnection(this EntityTypeBuilder<UnitLevel> builder)
        {
            builder.HasKey(e => new { e.UnitId, e.Level });

            builder.HasOne(e => e.Unit)
                .WithMany(e => e.UnitLevels)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<UnitPriceMaterial> UnitPriceMaterialConnection(this EntityTypeBuilder<UnitPriceMaterial> builder)
        {
            builder.HasKey(e => new { e.UnitId, e.MaterialId });

            builder.HasOne(e => e.Unit)
                .WithMany(e => e.UnitPriceMaterials)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Material)
                .WithMany(e => e.UnitPriceMaterials)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<Upgrade> UpgradeConnection(this EntityTypeBuilder<Upgrade> builder)
        {
            builder.HasMany(e => e.PlanetUpgrades)
                .WithOne(e => e.Upgrade)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.UpgradePriceMaterials)
                .WithOne(e => e.Upgrade)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<UpgradePriceMaterial> UpgradePriceMaterialConnection(this EntityTypeBuilder<UpgradePriceMaterial> builder)
        {
            builder.HasKey(e => new { e.UpgradeId, e.MaterialId });

            builder.HasOne(e => e.Material)
                .WithMany(e => e.UpgradePriceMaterials)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Upgrade)
                .WithMany(e => e.UpgradePriceMaterials)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static EntityTypeBuilder<User> UserConnection(this EntityTypeBuilder<User> builder)
        {
            builder.HasOne(e => e.Empire)
                .WithOne(e => e.Owner)
                .OnDelete(DeleteBehavior.NoAction);

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

            // builder.ApplyConfiguration(new EventEntityConfiguration()); ---> ez nem jó sajnos
            builder.Entity<BadHarvestEvent>().HasData(new BadHarvestEvent(){ Id = 1 });
            builder.Entity<GoodHarvestEvent>().HasData(new GoodHarvestEvent(){ Id = 2 });
            builder.Entity<JackpotEvent>().HasData(new JackpotEvent(){ Id = 3 });
            builder.Entity<SatisfiedPeopleEvent>().HasData(new SatisfiedPeopleEvent(){ Id = 4 });
            builder.Entity<UnsatisfiedPeopleEvent>().HasData(new UnsatisfiedPeopleEvent(){ Id = 5 });
            builder.Entity<SatisfiedUnitsEvent>().HasData(new SatisfiedUnitsEvent(){ Id = 6 });
            builder.Entity<UnsatisfiedUnitsEvent>().HasData(new UnsatisfiedUnitsEvent(){ Id = 7 });

            // builder.ApplyConfiguration(new PlanetEntityConfiguration()); ---> ez nem jó sajnos
            builder.Entity<AvypsoPlanet>().HasData(new AvypsoPlanet(1));
            builder.Entity<C137EarthPlanet>().HasData(new C137EarthPlanet(2));
            builder.Entity<CribatunePlanet>().HasData(new CribatunePlanet(3));
            builder.Entity<DarvisPlanet>().HasData(new DarvisPlanet(4));
            builder.Entity<DillonPlanet>().HasData(new DillonPlanet(5));
            builder.Entity<GingeriaPlanet>().HasData(new GingeriaPlanet(6));
            builder.Entity<HeolaraPlanet>().HasData(new HeolaraPlanet(7));
            builder.Entity<NusobosPlanet>().HasData(new NusobosPlanet(8));
            builder.Entity<SidataniaPlanet>().HasData(new SidataniaPlanet(9));
            builder.Entity<YoiphusPlanet>().HasData(new YoiphusPlanet(10));
            builder.Entity<ZuccarsPlanet>().HasData(new ZuccarsPlanet(11));

            builder.ApplyConfiguration(new PlanetPriceMaterialEntityConfiguration());
            builder.ApplyConfiguration(new PlanetPropertyEntityConfiguration());

            // builder.ApplyConfiguration(new UpgradeEntityConfiguration()); - ez nem jó sajnos
            builder.Entity<FuturisticResidentialAreaUpgrade>().HasData(new FuturisticResidentialAreaUpgrade() { Id = 1 });
            builder.Entity<InterdimensionalGastrogardenUpgrade>().HasData(new InterdimensionalGastrogardenUpgrade() { Id = 2 });
            builder.Entity<KineticShieldUpgrade>().HasData(new KineticShieldUpgrade() { Id = 3 });
            builder.Entity<LaserWeaponsUpgrade>().HasData(new LaserWeaponsUpgrade() { Id = 4 });
            builder.Entity<QuartzMineUpgrade>().HasData(new QuartzMineUpgrade() { Id = 5 });
            builder.Entity<SecretMilitaryBaseUpgrade>().HasData(new SecretMilitaryBaseUpgrade() { Id = 6 });
            builder.Entity<VibraniumArmorUpgrade>().HasData(new VibraniumArmorUpgrade() { Id = 7 });
            builder.Entity<VideocardExpansionUpgrade>().HasData(new VideocardExpansionUpgrade() { Id = 8 });

            builder.ApplyConfiguration(new UpgradePriceMaterialEntityConfiguration());

            builder.ApplyConfiguration(new UserEntityConfiguration());
            builder.ApplyConfiguration(new RoleEntityConfiguration());
            builder.ApplyConfiguration(new UserRoleEntityConfiguration());

            builder.ApplyConfiguration(new UnitEntityConfiguration());
            builder.ApplyConfiguration(new UnitLevelEntityConfiguration());
            builder.ApplyConfiguration(new UnitPriceMaterialEntityConfiguration());

            builder.ApplyConfiguration(new EmpireEntityConfiguration());
            builder.ApplyConfiguration(new EmpireMaterialEntityConfiguration());
            builder.ApplyConfiguration(new EmpireUnitEntityConfiguration());
        }
    }
}
