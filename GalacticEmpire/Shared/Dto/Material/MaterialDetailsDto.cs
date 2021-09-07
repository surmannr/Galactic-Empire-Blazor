using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Material
{
    public class MaterialDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Production { get; set; }
        public int Amount { get; set; }
        public string ImageUrl { get; set; }
    }
}
