using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.User
{
    public class AttackableUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public Guid EmpireId { get; set; }
    }
}
