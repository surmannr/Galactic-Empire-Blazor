using GalacticEmpire.Domain.Models.MaterialModel.Base;
using GalacticEmpire.Domain.Models.UnitModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.UnitModel
{
    public class UnitPriceMaterial
    {
        public int Amount { get; set; }

        public Guid MaterialId { get; set; }
        public Material Material { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}
