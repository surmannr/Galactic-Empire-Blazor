using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Unit
{
    public class BuyUnitsCollectionDto
    {
        public ICollection<BuyUnitDetailsDto> Units { get; set; }
    }
}
