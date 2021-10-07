using GalacticEmpire.Shared.Dto.Unit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Unit.Events
{
    public class UnitTrainingTimeEvent : INotification
    {
        public BuyUnitsCollectionDto UnitsCollection { get; set; }
        public Guid EmpireId { get; set; }
    }
}
