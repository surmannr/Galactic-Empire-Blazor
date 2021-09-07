using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Drone
{
    public class SendDroneDto
    {
        public Guid DronedEmpireId { get; set; }
        public int NumberOfDrones { get; set; }
    }
}
