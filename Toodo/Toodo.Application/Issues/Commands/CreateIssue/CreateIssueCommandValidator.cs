using FluentValidation;

namespace Toodo.Application.Issues.Commands.CreateIssue;

public class CreateIssueCommandValidator : AbstractValidator<CreateIssueCommand.CreateIssueCommand>
{
    public CreateIssueCommandValidator()
    {
        RuleFor(c => c.Title)
            .Length(3, 100)
            .WithMessage("Title must be between 3 and 100 characters");

        RuleFor(c => c.Description)
            .MaximumLength(1000)
            .WithMessage("Description must be less than 1000 characters");
    }
}