using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Constants.Time
{
    public static class TimeConstants
    {
        // Upgrade time
        public static readonly TimeSpan UpgradeTime = new(0, 1, 0);

        // Planet capturing time
        public static readonly TimeSpan PlanetCaptureTime = new(0, 5, 0);
    }
}
