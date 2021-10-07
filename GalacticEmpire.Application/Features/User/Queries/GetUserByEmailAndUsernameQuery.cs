using FluentValidation;
using GalacticEmpire.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.User.Queries
{
    public static class GetUserByEmailAndUsernameQuery
    {
        public class Query : IRequest<Domain.Models.UserModel.Base.User>
        {
            public string UserName { get; set; }
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<Query, Domain.Models.UserModel.Base.User>
        {
            private readonly GalacticEmpireDbContext dbContext;

            public Handler(GalacticEmpireDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<Domain.Models.UserModel.Base.User> Handle(Query request, CancellationToken cancellationToken)
            {
                return await dbContext.Users.Where(u => u.UserName == request.UserName && u.Email == request.Email).FirstOrDefaultAsync();
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {

            }
        }
    }
}
