using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Alliance
{
    public class AlliancePreviewDto
    {
        public Guid Id { get; set; }
        public string Name {  get; set; }
        public int MembersCount { get; set; }
    }
}
