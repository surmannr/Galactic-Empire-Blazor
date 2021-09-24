using GalacticEmpire.Shared.Dto.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Planet
{
    public class PlanetDto
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public string Description {  get; set; }
        public string ImageUrl { get; set; }
        public TimeSpan CapturingTime { get; set; }

        public PlanetPropertyDto PlanetProperty { get; set; }
        public ICollection<PriceMaterialDto> RequiredMaterials { get; set; }
    }
}
