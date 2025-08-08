using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toodo.Domain.Entities;
using Toodo.Infrastructure.Data;

namespace Toodo.Application.Issues.Queries.GetIssues;

public class GetIssuesQuery : IRequest<IEnumerable<IssueSummaryResponse>>
{
}

internal class GetIssuesQueryHandler(
    ILogger<GetIssuesQueryHandler> logger,
    IMapper mapper,
    ApplicationDbContext context
) : IRequestHandler<GetIssuesQuery, IEnumerable<IssueSummaryResponse>>
{
    public async Task<IEnumerable<IssueSummaryResponse>> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting issues");
        var issues = await context
            .Issues
            .ProjectTo<IssueSummaryResponse>(mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        return issues;
    }
}