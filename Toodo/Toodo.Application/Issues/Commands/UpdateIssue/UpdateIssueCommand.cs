using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toodo.Application.Common.Exceptions;
using Toodo.Domain.Entities;
using Toodo.Infrastructure.Data;

namespace Toodo.Application.Issues.Commands.UpdateIssue;

public class UpdateIssueCommand : IRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class UpdateIssueCommandHandler(
    ILogger<UpdateIssueCommandHandler> logger,
    IMapper mapper,
    ApplicationDbContext context
) : IRequestHandler<UpdateIssueCommand>
{
    public async Task Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating issue with id = {id}", request.Id);

        var issue = await context.Issues.FirstOrDefaultAsync(e => e.Id == request.Id,
            cancellationToken: cancellationToken);

        if (issue is null)
        {
            throw new NotFoundException(nameof(Issue), request.Id);
        }

        var updated = mapper.Map(request, issue);
        context.Issues.Update(updated);
        await context.SaveChangesAsync(cancellationToken);
    }
}