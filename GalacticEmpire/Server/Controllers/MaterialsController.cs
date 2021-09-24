using GalacticEmpire.Application.Features.Material.Queries;
using GalacticEmpire.Shared.Dto.Material;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticEmpire.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMediator mediator;

        public MaterialsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<MaterialDto>> GetAllMaterials([FromQuery] GetAllMaterialsQuery.Query query)
        {
            return await mediator.Send(query);
        }
    }
}
