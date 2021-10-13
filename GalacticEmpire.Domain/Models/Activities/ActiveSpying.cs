using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.Activities
{
    public class ActiveSpying
    {
        public Guid EmpireId {  get; set; }

        public DateTimeOffset EndDate {  get; set; }

        public string DefenderEmpireName { get; set; }
    }
}
