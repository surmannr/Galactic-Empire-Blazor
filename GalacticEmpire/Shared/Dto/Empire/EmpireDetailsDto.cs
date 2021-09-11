﻿using GalacticEmpire.Shared.Dto.Event;
using GalacticEmpire.Shared.Dto.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Shared.Dto.Empire
{
    public class EmpireDetailsDto
    {
        public string Name { get; set; }
        public int MaxNumberOfUnits { get; set; }
        public int MaxNumberOfPopulation { get; set; }
        public int Population { get; set; }

        public string AllianceName { get; set; }
        public bool AllianceInvitationRight { get; set; }

        public IEnumerable<EmpirePlanetDto> Planets { get; set; }
        public ICollection<BattleUnitDto> Units { get; set; }
        public EventDto Event { get; set; }
    }
}