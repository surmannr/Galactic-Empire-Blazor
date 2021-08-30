using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Constants.Event
{
    public static class EventTypeConstants
    {
        public static readonly string Base = "event_base";

        // BadHarvest
        public static readonly string BadHarvestType = "event_badharvest";

        // GoodHarvest
        public static readonly string GoodHarvestType = "event_goodharvest";
        
        // SatisfiedPeople
        public static readonly string SatisfiedPeopleType = "event_satisfiedpeople";

        // UnsatisfiedPeople
        public static readonly string UnsatisfiedPeopleType = "event_unsatisfiedpeople";

        // Jackpot
        public static readonly string JackpotType = "event_jackpot";

        // SatisfiedUnits
        public static readonly string SatisfiedUnitsType = "event_satisfiedunits";

        // UnsatisfiedUnits
        public static readonly string UnsatisfiedUnitsType = "event_unsatisfiedunits";
    }
}
