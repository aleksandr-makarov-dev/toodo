using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toodo.Application.Common.Exceptions;
using Toodo.Domain.Entities;
using Toodo.Infrastructure.Data;

namespace Toodo.Application.Issues.Commands.DeleteIssue;

public class DeleteIssueCommand(int id) : IRequest
{
    public int Id { get; } = id;
}

public class DeleteIssueCommandHandler(
    ILogger<DeleteIssueCommandHandler> logger,
    ApplicationDbContext context
) : IRequestHandler<DeleteIssueCommand>
{
    public async Task Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting issue with id = {Id}", request.Id);
        var issue = await context.Issues.FirstOrDefaultAsync(e => e.Id == request.Id,
            cancellationToken: cancellationToken);

        if (issue is null)
        {
            throw new NotFoundException(nameof(Issue), request.Id);
        }

        context.Issues.Remove(issue);
        await context.SaveChangesAsync(cancellationToken);
    }
}