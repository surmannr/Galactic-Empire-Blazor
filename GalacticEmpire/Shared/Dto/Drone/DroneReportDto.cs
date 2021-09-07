using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Drone
{
    public class DroneReportDto
    {
        public Guid DroneReportId { get; set; }
        public string DronedEmpireName { get; set; }
        public int NumberOfDrones { get; set; }
        public Guid? WinnerEmpireId { get; set; }
        public int? DefensePoints { get; set; }
    }
}
