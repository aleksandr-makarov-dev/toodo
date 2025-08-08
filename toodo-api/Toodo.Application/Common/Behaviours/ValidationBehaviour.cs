using FluentValidation;
using MediatR;
using ValidationException = Toodo.Application.Common.Exceptions.ValidationException;

namespace Toodo.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var validationResults =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(request, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Count != 0)
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count > 0)
                throw new ValidationException(failures);
        }

        return await next(cancellationToken);
    }
}