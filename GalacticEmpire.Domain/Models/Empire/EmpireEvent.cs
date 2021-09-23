using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.EventModel.Base;
using System;

namespace GalacticEmpire.Domain.Models.EmpireModel
{
    public class EmpireEvent : BaseModel<Guid>
    {
        public DateTimeOffset Date { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public Guid EmpireId { get; set; }
        public Empire Empire { get; set; }
    }
}
