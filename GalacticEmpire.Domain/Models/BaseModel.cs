using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models
{
    public abstract class BaseModel<T>
    {
        public T Id { get; set; }
    }

    public abstract class BaseAttackModel<T, K> : BaseModel<T>
    {
        public DateTimeOffset Date { get; set; }

        public Guid AttackerId { get; set; }
        public K Attacker { get; set; }

        public Guid DefenderId { get; set; }
        public K Defender { get; set; }

        public Guid? WinnerId { get; set; }
    }
}
