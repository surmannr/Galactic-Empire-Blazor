using GalacticEmpire.Shared.Dto.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Unit
{
    public class UnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UnitLevelDto> UnitLevels { get; set; }
        public int MercenaryPerHour { get; set; }
        public int SupplyPerHour { get; set; }
        public int RankPoint { get; set; }
        public ICollection<PriceMaterialDto> RequiredMaterials { get; set; }
        public string ImageUrl { get; set; }
    }
}
