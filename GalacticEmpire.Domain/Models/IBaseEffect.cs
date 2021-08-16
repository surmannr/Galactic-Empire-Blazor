using GalacticEmpire.Domain.Models.EmpireModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models
{
    public interface IBaseEffect<T>
    {
        public void ApplyEffect(T model);
        public void RemoveEffect(T model);
    }
}
