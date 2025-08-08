using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toodo.Application.Common.Exceptions;
using Toodo.Domain.Entities;
using Toodo.Infrastructure.Data;

namespace Toodo.Application.Issues.Queries.GetIssue;

public class GetIssueQuery(int id) : IRequest<IssueDetailsResponse>
{
    public int Id { get; } = id;
}

public class GetIssueQueryHandler(
    ILogger<GetIssueQueryHandler> logger,
    IMapper mapper,
    ApplicationDbContext context
) : IRequestHandler<GetIssueQuery, IssueDetailsResponse>
{
    public async Task<IssueDetailsResponse> Handle(GetIssueQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting issue with id = {Id}", request.Id);

        var issue = await context.Issues.FirstOrDefaultAsync(e => e.Id == request.Id,
            cancellationToken: cancellationToken);

        if (issue is null)
        {
            throw new NotFoundException(nameof(Issue), request.Id);
        }

        return mapper.Map<IssueDetailsResponse>(issue);
    }
}