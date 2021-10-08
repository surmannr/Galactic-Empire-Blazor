using AutoMapper;
using GalacticEmpire.Domain.Models.AllianceModel;
using GalacticEmpire.Domain.Models.AllianceModel.Base;
using GalacticEmpire.Domain.Models.AttackModel;
using GalacticEmpire.Domain.Models.AttackModel.Base;
using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.EventModel.Base;
using GalacticEmpire.Domain.Models.MaterialModel.Base;
using GalacticEmpire.Domain.Models.PlanetModel;
using GalacticEmpire.Domain.Models.PlanetModel.Base;
using GalacticEmpire.Domain.Models.UnitModel;
using GalacticEmpire.Domain.Models.UnitModel.Base;
using GalacticEmpire.Domain.Models.UpgradeModel;
using GalacticEmpire.Domain.Models.UpgradeModel.Base;
using GalacticEmpire.Domain.Models.UserModel.Base;
using GalacticEmpire.Shared.Dto.Alliance;
using GalacticEmpire.Shared.Dto.Attack;
using GalacticEmpire.Shared.Dto.Empire;
using GalacticEmpire.Shared.Dto.Event;
using GalacticEmpire.Shared.Dto.Material;
using GalacticEmpire.Shared.Dto.Planet;
using GalacticEmpire.Shared.Dto.Time;
using GalacticEmpire.Shared.Dto.Unit;
using GalacticEmpire.Shared.Dto.Upgrade;
using GalacticEmpire.Shared.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Event, EventDto>();

            CreateMap<Material, MaterialDto>();

            CreateMap<TimeSpan, TimeDto>()
                .ForMember(
                    tdto => tdto.Hour, time => time.MapFrom(t => t.Hours)
                )
                .ForMember(
                    tdto => tdto.Minute, time => time.MapFrom(t => t.Minutes)
                )
                .ForMember(
                    tdto => tdto.Second, time => time.MapFrom(t => t.Seconds)
                );

            CreateMap<PlanetProperty, PlanetPropertyDto>();

            CreateMap<UpgradePriceMaterial, PriceMaterialDto>()
                .ForMember(
                    pmdto => pmdto.Id, upm => upm.MapFrom(u => u.Material.Id)
                )
                .ForMember(
                    pmdto => pmdto.Name, upm => upm.MapFrom(u => u.Material.Name)
                )
                .ForMember(
                    pmdto => pmdto.ImageUrl, upm => upm.MapFrom(u => u.Material.ImageUrl)
                );

            CreateMap<PlanetPriceMaterial, PriceMaterialDto>()
                .ForMember(
                    pmdto => pmdto.Id, upm => upm.MapFrom(u => u.Material.Id)
                )
                .ForMember(
                    pmdto => pmdto.Name, upm => upm.MapFrom(u => u.Material.Name)
                )
                .ForMember(
                    pmdto => pmdto.ImageUrl, upm => upm.MapFrom(u => u.Material.ImageUrl)
                );

            CreateMap<UnitPriceMaterial, PriceMaterialDto>()
                .ForMember(
                    pmdto => pmdto.Id, upm => upm.MapFrom(u => u.Material.Id)
                )
                .ForMember(
                    pmdto => pmdto.Name, upm => upm.MapFrom(u => u.Material.Name)
                )
                .ForMember(
                    pmdto => pmdto.ImageUrl, upm => upm.MapFrom(u => u.Material.ImageUrl)
                );

            CreateMap<Planet, PlanetDto>()
                .ForMember(
                    planetDto => planetDto.RequiredMaterials,
                    planet => planet.MapFrom(u => u.PlanetPriceMaterials)
                )
                .ForMember(
                    planetDto => planetDto.CapturingTime,
                    planet => planet.MapFrom(u => u.CapturingTime)
                );

            CreateMap<Upgrade, UpgradeDto>()
                .ForMember(
                    upgradeDto => upgradeDto.RequiredMaterials,
                    upgrade => upgrade.MapFrom(u => u.UpgradePriceMaterials)
                );

            CreateMap<UnitLevel, UnitLevelDto>();

            CreateMap<Unit, UnitDto>()
                .ForMember(
                    unitDto => unitDto.RequiredMaterials,
                    unit => unit.MapFrom(u => u.UnitPriceMaterials)
                );

            CreateMap<User,UserRankDto>()
                .ForMember(
                    urdto => urdto.EmpireName,
                    user => user.MapFrom(u => u.Empire.Name)
                );

            CreateMap<EmpirePlanetUpgrade, EmpirePlanetUpgradeDto>()
                .ForMember(
                    epudto => epudto.UpgradeName,
                    epu => epu.MapFrom(e => e.Upgrade.Name)
                )
                .ForMember(
                    epudto => epudto.UpgradeDescription,
                    epu => epu.MapFrom(e => e.Upgrade.Description)
                )
                .ForMember(
                    epudto => epudto.RemainingTime,
                    epu => epu.MapFrom(e => e.Upgrade.UpgradeTime)
                );

            CreateMap<EmpireEvent, EventDto>()
                .ForMember(
                    edto => edto.Name,
                    ee => ee.MapFrom(e => e.Event.Name)
                )
                .ForMember(
                    edto => edto.Description,
                    ee => ee.MapFrom(e => e.Event.Description)
                );

            CreateMap<EmpireMaterial, MaterialDetailsDto>()
                .ForMember(
                    edto => edto.Id,
                    ee => ee.MapFrom(e => e.Material.Id)
                )
                .ForMember(
                    edto => edto.Name,
                    ee => ee.MapFrom(e => e.Material.Name)
                )
                .ForMember(
                    edto => edto.Amount,
                    ee => ee.MapFrom(e => e.Amount)
                )
                .ForMember(
                    edto => edto.Production,
                    ee => ee.MapFrom(e => e.BaseProduction * e.ProductionMultiplier)
                )
                .ForMember(
                    edto => edto.ImageUrl,
                    ee => ee.MapFrom(e => e.Material.ImageUrl)
                );

            CreateMap<EmpirePlanet, EmpirePlanetDto>()
                .ForMember(
                    epdto => epdto.PlanetProperty,
                    ep => ep.MapFrom(e => e.Planet.PlanetProperty)
                )
                .ForMember(
                    epdto => epdto.Name,
                    ep => ep.MapFrom(e => e.Planet.Name)
                )
                .ForMember(
                    epdto => epdto.Description,
                    ep => ep.MapFrom(e => e.Planet.Description)
                )
                .ForMember(
                    epdto => epdto.ImageUrl,
                    ep => ep.MapFrom(e => e.Planet.ImageUrl)
                )
                .ForMember(
                    epdto => epdto.Upgrades,
                    ep => ep.MapFrom(e => e.EmpirePlanetUpgrades)
                );

            CreateMap<EmpireUnit, BattleUnitDto>()
                .ForMember(
                    budto => budto.ImageUrl,
                    eu => eu.MapFrom(e => e.Unit.ImageUrl)
                )
                .ForMember(
                    budto => budto.Count,
                    eu => eu.MapFrom(e => e.Amount)
                )
                .ForMember(
                    budto => budto.Id,
                    eu => eu.MapFrom(e => e.Unit.Id)
                )
                .ForMember(
                    budto => budto.Name,
                    eu => eu.MapFrom(e => e.Unit.Name)
                );

            CreateMap<Empire, EmpireDetailsDto>()
                .ForMember(
                    edto => edto.AllianceName,
                    empire => empire.MapFrom(e => e.Alliance.Alliance.Name)
                )
                .ForMember(
                    edto => edto.AllianceInvitationRight,
                    empire => empire.MapFrom(e => e.Alliance.InvitationRight)
                )
                .ForMember(
                    edto => edto.Planets,
                    empire => empire.MapFrom(e => e.EmpirePlanets)
                )
                .ForMember(
                    edto => edto.Units,
                    empire => empire.MapFrom(e => e.EmpireUnits)
                )
                .ForMember(
                    edto => edto.Materials,
                    empire => empire.MapFrom(e => e.EmpireMaterials)
                )
                .ForMember(
                    edto => edto.Event,
                    empire => empire.MapFrom(e => e.EmpireEvents.OrderByDescending(s => s.Date).FirstOrDefault())
                );

            CreateMap<AllianceInvitation, AllianceInvitationDto>()
                .ForMember(
                    aidto => aidto.AllianceName,
                    ai => ai.MapFrom(a => a.Alliance.Name)
                )
                .ForMember(
                    aidto => aidto.InviterEmpireName,
                    ai => ai.MapFrom(a => a.InviterEmpire.Name)
                )
                .ForMember(
                    aidto => aidto.MembersCount,
                    ai => ai.MapFrom(a => a.Alliance.Members.Count())
                )
                .ForMember(
                    aidto => aidto.Date,
                    ai => ai.MapFrom(a => a.Date)
                );

            CreateMap<AllianceMember, AllianceMemberDto>()
                .ForMember(
                    amdto => amdto.EmpireName,
                    am => am.MapFrom(a => a.Empire.Name)
                )
                .ForMember(
                    amdto => amdto.RankPoint,
                    am => am.MapFrom(a => a.Empire.Owner.Points)
                );

            CreateMap<Alliance, AllianceDetailsDto>()
                .ForMember(
                    amdto => amdto.Members,
                    am => am.MapFrom(a => a.Members)
                );

            CreateMap<Alliance, AlliancePreviewDto>()
                .ForMember(
                    aidto => aidto.MembersCount,
                    ai => ai.MapFrom(a => a.Members.Count())
                );

            CreateMap<User, AttackableUserDto>()
                .ForMember(
                    audto => audto.EmpireName,
                    ai => ai.MapFrom(a => a.Empire.Name)
                )
                .ForMember(
                    audto => audto.EmpireId,
                    ai => ai.MapFrom(a => a.Empire.Id)
                );

            CreateMap<AttackUnit, BattleUnitDto>()
                .ForMember(
                    budto => budto.Id,
                    au => au.MapFrom(a => a.Unit.Id)
                )
                .ForMember(
                    budto => budto.Name,
                    au => au.MapFrom(a => a.Unit.Name)
                )
                .ForMember(
                    budto => budto.Count,
                    au => au.MapFrom(a => a.Amount)
                )
                .ForMember(
                    budto => budto.ImageUrl,
                    au => au.MapFrom(a => a.Unit.ImageUrl)
                );

            CreateMap<DefenseUnit, BattleUnitDto>()
                .ForMember(
                    budto => budto.Id,
                    au => au.MapFrom(a => a.Unit.Id)
                )
                .ForMember(
                    budto => budto.Name,
                    au => au.MapFrom(a => a.Unit.Name)
                )
                .ForMember(
                    budto => budto.Count,
                    au => au.MapFrom(a => a.Amount)
                )
                .ForMember(
                    budto => budto.ImageUrl,
                    au => au.MapFrom(a => a.Unit.ImageUrl)
                );
        }
    }
}
