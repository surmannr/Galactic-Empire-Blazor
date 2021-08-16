using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.MaterialModel.Base
{
    public class Material : BaseModel<Guid>
    {
        public string Name { get; set; }
    }
}
