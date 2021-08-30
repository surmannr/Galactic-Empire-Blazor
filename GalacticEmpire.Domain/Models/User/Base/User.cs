using GalacticEmpire.Domain.Models.EmpireModel.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace GalacticEmpire.Domain.Models.UserModel.Base
{
    public class User : IdentityUser
    {
        public int Points { get; set; }

        public Guid EmpireId { get; set; }
        public Empire Empire { get; set; }
    }
}
