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
        public int MercenaryPerRound { get; set; }
        public int SupplyPerRound { get; set; }
        public int RankPoint { get; set; }
        public ICollection<MaterialDto> RequiredMaterials { get; set; }
        public int CurrentCount { get; set; }
        public string ImageUrl { get; set; }
    }
}
