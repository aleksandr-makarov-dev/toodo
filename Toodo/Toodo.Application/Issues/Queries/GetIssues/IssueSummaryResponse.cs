namespace Toodo.Application.Issues.Queries.GetIssues;

public class IssueSummaryResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}