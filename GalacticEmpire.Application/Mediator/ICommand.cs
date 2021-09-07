using MediatR;

namespace GalacticEmpire.Application.Mediator
{
    public interface ICommand<TResult> : IRequest<TResult>
    {
    }
}