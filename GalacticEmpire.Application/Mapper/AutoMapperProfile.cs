using AutoMapper;
using GalacticEmpire.Domain.Models.EventModel.Base;
using GalacticEmpire.Shared.Dto.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Event, EventDto>();
        }
    }
}
