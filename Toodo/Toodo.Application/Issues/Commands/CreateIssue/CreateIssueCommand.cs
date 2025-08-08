using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Toodo.Domain.Entities;
using Toodo.Infrastructure.Data;

namespace Toodo.Application.Issues.Commands.CreateIssueCommand;

public class CreateIssueCommand : IRequest<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

internal class CreateIssueCommandHandler(
    ILogger<CreateIssueCommandHandler> logger,
    IMapper mapper,
    ApplicationDbContext context
) : IRequestHandler<CreateIssueCommand, int>
{
    public async Task<int> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new issue with Title = {title}", request.Title);
        var issue = mapper.Map<CreateIssueCommand, Issue>(request);
        context.Issues.Add(issue);
        await context.SaveChangesAsync(cancellationToken);
        return issue.Id;
    }
}