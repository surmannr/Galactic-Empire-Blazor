using GalacticEmpire.Domain.Models.EmpireModel.Base;
using System;

namespace GalacticEmpire.Domain.Models.UpgradeModel.Base
{
    public class Upgrade : BaseModel<Guid>, IBaseEffect<Empire>
    {
        public string Name { get; set; }
        public TimeSpan UpgradeTime { get; set; }

        public virtual void ApplyEffect(Empire model) { }

        public virtual void RemoveEffect(Empire model) { }
    }
}
