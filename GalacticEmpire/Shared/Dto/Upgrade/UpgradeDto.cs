using GalacticEmpire.Shared.Dto.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Upgrade
{
    public class UpgradeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool DoesExist { get; set; }
        public TimeSpan UpgradeTime { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<MaterialDto> RequiredMaterials { get; set; }
    }
}
