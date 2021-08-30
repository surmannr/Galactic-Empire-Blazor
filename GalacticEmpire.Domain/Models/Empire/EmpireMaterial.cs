using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Domain.Models.MaterialModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.EmpireModel
{
    public class EmpireMaterial
    {
        public double BaseProduction { get; set; }
        public double ProductionMultiplier { get; set; }
        public int Amount { get; set; }

        public Guid EmpireId { get; set; }
        public Empire Empire { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
