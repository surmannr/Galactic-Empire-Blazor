using GalacticEmpire.Domain.Models.EmpireModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.PlanetModel.Base
{
    public class Planet : BaseModel<Guid>, IBaseEffect<Empire>
    {
        public string Name { get; set; }
        public TimeSpan CapturingTime { get; set; }

        public virtual void ApplyEffect(Empire model) { }

        public virtual void RemoveEffect(Empire model) { }
    }
}
