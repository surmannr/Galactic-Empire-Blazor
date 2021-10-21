using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Drone
{
    public class DroneReportDetailsDto
    {
        public Guid Id { get; set; }
        public string DronedEmpireName { get; set; }
        public DateTimeOffset Date { get; set; }
        public string OpponentEmpireName { get; set; }
        public int NumberOfAttackerDrones { get; set; }
        public int NumberOfDefenderDrones { get; set; }
        public Guid? WinnerEmpireId { get; set; }
        public string WinnerEmpireName { get; set; }
        public int? DefensePoints { get; set; }
        public bool IsAttacker { get; set; }
    }
}
