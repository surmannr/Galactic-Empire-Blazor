using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.Activities
{
    public class ActiveTraining
    {
        public Guid EmpireId {  get; set; }

        public DateTimeOffset EndDate {  get; set; }

        public string UnitName { get; set; }
        public int UnitLevel { get; set; }
        public int UnitAmount { get; set; }
    }
}
