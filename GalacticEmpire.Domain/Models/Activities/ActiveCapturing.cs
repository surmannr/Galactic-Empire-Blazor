using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.Activities
{
    public class ActiveCapturing
    {
        public Guid EmpireId {  get; set; }

        public DateTimeOffset EndDate {  get; set; }

        public string PlanetName { get; set; }
    }
}
