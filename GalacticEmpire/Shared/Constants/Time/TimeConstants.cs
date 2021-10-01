using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Constants.Time
{
    public static class TimeConstants
    {
        // Fejlesztési idő
        public static readonly TimeSpan UpgradeTime = new(0, 1, 0);

        // Bolygó elfoglalási idő
        public static readonly TimeSpan PlanetCaptureTime = new(0, 5, 0);

        // Támadás és kémkedés idő
        public static readonly TimeSpan AttackAndSpyingTime = new(0, 30, 0);
    }
}
