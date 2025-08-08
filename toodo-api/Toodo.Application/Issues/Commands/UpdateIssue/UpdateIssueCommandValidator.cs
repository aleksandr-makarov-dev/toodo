using FluentValidation;

namespace Toodo.Application.Issues.Commands.UpdateIssue;

public class UpdateIssueCommandValidator : AbstractValidator<UpdateIssueCommand>
{
    public UpdateIssueCommandValidator()
    {
        RuleFor(c => c.Title)
            .Length(3, 100)
            .WithMessage("Title must be between 3 and 100 characters");

        RuleFor(c => c.Description)
            .MaximumLength(1000)
            .WithMessage("Description must be less than 1000 characters");
    }
}