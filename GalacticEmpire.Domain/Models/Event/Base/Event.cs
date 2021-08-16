using GalacticEmpire.Domain.Models.EmpireModel.Base;
using System;

namespace GalacticEmpire.Domain.Models.EventModel.Base
{
    public abstract class Event : BaseModel<Guid>, IBaseEffect<Empire>
    {
        public string Name { get; set; }

        public virtual void ApplyEffect(Empire empire) { }

        public virtual void RemoveEffect(Empire empire) { }
    }
}
