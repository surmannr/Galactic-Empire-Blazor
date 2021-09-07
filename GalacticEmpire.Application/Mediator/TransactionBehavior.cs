using GalacticEmpire.Dal;
using MediatR;

namespace GalacticEmpire.Application.Mediator
{
    public class TransactionBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
         where TRequest : ICommand<TResult>
    {
        private readonly GalacticEmpireDbContext _dbContext;

        public TransactionBehavior(GalacticEmpireDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
        {
            using var tran = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await next();
                await tran.CommitAsync();
                return result;
            }
            catch (System.Exception)
            {
                await tran.RollbackAsync();
                throw;
            }
        }
    }
}
