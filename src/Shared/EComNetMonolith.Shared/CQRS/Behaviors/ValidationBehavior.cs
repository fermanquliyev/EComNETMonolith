using FluentValidation;
using MediatR;

namespace EComNetMonolith.Shared.CQRS.Behaviors;

public class ValidationBehavior<TCommand, TCommandResponse> : IPipelineBehavior<TCommand, TCommandResponse>
    where TCommand : IRequest<TCommandResponse>
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TCommand>> validators)
    {
        _validators = validators;
    }
    public async Task<TCommandResponse> Handle(TCommand request, RequestHandlerDelegate<TCommandResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TCommand>(request);
        if (!_validators.Any())
        {
            return await next();
        }
        var validationResults =
            await Task.WhenAll(_validators
                .Select(v => v.ValidateAsync(context, cancellationToken))
                .ToList());

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
