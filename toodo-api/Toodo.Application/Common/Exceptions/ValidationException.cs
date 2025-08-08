using FluentValidation.Results;

namespace Toodo.Application.Common.Exceptions;

public class ValidationException() : Exception("One or more validation failures have occurred.")
{
    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(failure => failure.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failure => failure.Key, failure => failure.ToArray());
    }
}