using MediatR;

namespace EComNetMonolith.Shared.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}

public interface IQuery : IQuery<Unit>
{
}