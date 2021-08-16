using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Domain.Models.EmpireModel.Base;
using System;
using System.Collections.Generic;

namespace GalacticEmpire.Domain.Models.EventModel.Base
{
    public abstract class Event : BaseModel<Guid>, IBaseEffect<Empire>
    {
        public string Name { get; set; }

        public ICollection<EmpireEvent> EmpireEvents { get; set; }

        public virtual void ApplyEffect(Empire empire) { }

        public virtual void RemoveEffect(Empire empire) { }
    }
}
