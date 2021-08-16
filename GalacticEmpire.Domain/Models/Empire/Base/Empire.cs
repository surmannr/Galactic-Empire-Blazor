using GalacticEmpire.Domain.Models.UserModel.Base;
using System;

namespace GalacticEmpire.Domain.Models.EmpireModel.Base
{
    public class Empire : BaseModel<Guid>
    {
        public string Name { get; set; }
        public int MaxNumberOfUnits { get; set; }
        public int Population { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
