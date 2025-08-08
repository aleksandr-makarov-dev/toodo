using AutoMapper;
using Toodo.Application.Issues.Commands.CreateIssueCommand;
using Toodo.Application.Issues.Commands.UpdateIssue;
using Toodo.Application.Issues.Queries.GetIssue;
using Toodo.Application.Issues.Queries.GetIssues;
using Toodo.Domain.Entities;

namespace Toodo.Application.Issues;

public class IssueProfile : Profile
{
    public IssueProfile()
    {
        CreateMap<CreateIssueCommand, Issue>();
        CreateMap<Issue, IssueSummaryResponse>();
        CreateMap<UpdateIssueCommand, Issue>();
        CreateMap<Issue, IssueDetailsResponse>();
    }
}