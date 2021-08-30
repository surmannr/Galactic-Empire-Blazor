using GalacticEmpire.Domain.Models.AttackModel;
using GalacticEmpire.Domain.Models.EmpireModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.UnitModel.Base
{
    public class Unit : BaseModel<int>
    {
        public string Name { get; set; }
        public int MercenaryPerHour { get; set; }
        public int SupplyPerHour { get; set; }
        public int RankPoint { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<UnitLevel> UnitLevels { get; set; }
        public ICollection<EmpireUnit> EmpireUnits { get; set; }
        public ICollection<UnitPriceMaterial> UnitPriceMaterials { get; set; }
        public ICollection<AttackUnit> AttackUnits { get; set; }
    }
}
