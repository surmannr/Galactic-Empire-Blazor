using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.ExtensionsAndServices.PaginationExtensions;
using GalacticEmpire.Application.PaginationExtensions;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Alliance;
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
    public static class GetAllAlliancesQuery
    {
        public class Query : IRequest<PagedResult<AlliancePreviewDto>>
        {
            public PaginationData PaginationData { get; set; }
            public string Filter { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedResult<AlliancePreviewDto>>
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

            public async Task<PagedResult<AlliancePreviewDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var alliances = await dbContext.Alliances
                    .Include(a => a.Members)
                    .ToListAsync();

                var alliancesdto = alliances.AsQueryable()
                    .ProjectTo<AlliancePreviewDto>(mapper.ConfigurationProvider);

                // Filter ellenőrzés
                if (!string.IsNullOrEmpty(request.Filter) && !string.IsNullOrWhiteSpace(request.Filter))
                {
                    alliancesdto = alliancesdto.Where(u => u.Name.Contains(request.Filter));
                }

                return alliancesdto.ToPagedList(request.PaginationData.PageSize, request.PaginationData.PageNumber);
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.PaginationData).SetValidator(new PaginationRuleValidator()).When(x => x.PaginationData is not null);
            }
        }
    }
}
