using MediatR;

namespace EComNetMonolith.Shared.CQRS;
public interface ICommandHandler<in ICommand, TResponse> : IRequestHandler<ICommand, TResponse> where ICommand : IRequest<TResponse> where TResponse : notnull
{
}

public interface ICommandHandler<in ICommand> : IRequestHandler<ICommand, Unit> where ICommand : IRequest<Unit>
{
}
