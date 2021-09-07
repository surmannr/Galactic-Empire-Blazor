using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.User
{
    public class UserRankDto
    {
        public string UserName { get; set; }
        public string EmpireName { get; set; }
        public int Points { get; set; }
        public int Placement { get; set; }
    }
}
