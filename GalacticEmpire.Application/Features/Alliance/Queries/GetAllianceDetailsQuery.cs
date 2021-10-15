using AutoMapper;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.MediatorExtension;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Alliance;
using GalacticEmpire.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Alliance.Queries
{
    public static class GetAllianceDetailsQuery
    {
        public class Query : IRequest<AllianceDetailsDto>
        {
            public Guid AllianceId { get; set; }
        }

        public class Handler : IRequestHandler<Query, AllianceDetailsDto>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IIdentityService identityService;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IIdentityService identityService, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.identityService = identityService;
                this.mapper = mapper;
            }

            public async Task<AllianceDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var alliance = await dbContext.Alliances
                    .Include(e => e.Members)
                        .ThenInclude(e => e.Empire)
                            .ThenInclude(e => e.Owner)
                    .Where(a => a.Id == request.AllianceId)
                    .FirstOrDefaultAsync();

                if (alliance == null)
                {
                    throw new NotFoundException("Nem létezik ilyen szövetség.");
                }

                return mapper.Map<AllianceDetailsDto>(alliance);
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.AllianceId).NotNull().WithMessage("A szövetség azonosítója nem lehet üres.");
            }
        }
    }
}
