using MediatR;

namespace EComNetMonolith.Shared.CQRS;

public interface IQueryHandler<in IQuery, TResponse> : IRequestHandler<IQuery, TResponse>
    where IQuery : IQuery<TResponse>
    where TResponse : notnull
{
}

public interface IQueryHandler<in IQuery> : IRequestHandler<IQuery, Unit>
    where IQuery : IQuery<Unit>
{
}
