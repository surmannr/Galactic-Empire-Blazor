using MediatR;

namespace GalacticEmpire.Application.MediatorExtension
{
    public interface ICommand<TResult> : IRequest<TResult>
    {
    }
}