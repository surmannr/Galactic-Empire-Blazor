using GalacticEmpire.Domain.Models.AllianceModel;
using GalacticEmpire.Domain.Models.AllianceModel.Base;
using GalacticEmpire.Domain.Models.AttackModel;
using GalacticEmpire.Domain.Models.AttackModel.Base;
using GalacticEmpire.Domain.Models.UserModel.Base;
using System;
using System.Collections.Generic;

namespace GalacticEmpire.Domain.Models.EmpireModel.Base
{
    public class Empire : BaseModel<Guid>
    {
        public string Name { get; set; }
        public int MaxNumberOfUnits { get; set; }
        public int MaxNumberOfPopulation { get; set; }
        public int Population { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public AllianceMember Alliance { get; set; }

        public Guid? OwnedAllianceId { get; set; }
        public Alliance OwnedAlliance { get; set; }

        public ICollection<EmpireEvent> EmpireEvents { get; set; }
        public ICollection<EmpireMaterial> EmpireMaterials { get; set; }
        public ICollection<EmpirePlanet> EmpirePlanets { get; set; }
        public ICollection<EmpireUnit> EmpireUnits { get; set; }

        public ICollection<Attack> AttackerAttack { get; set; }
        public ICollection<Attack> DefenderAttack { get; set; }

        public ICollection<DroneAttack> DroneAttackerAttack { get; set; }
        public ICollection<DroneAttack> DroneDefenderAttack { get; set; }

        public ICollection<AllianceInvitation> AllianceReceivedInvitations { get; set; }
        public ICollection<AllianceInvitation> AllianceSentInvitations { get; set; }
    }
}
