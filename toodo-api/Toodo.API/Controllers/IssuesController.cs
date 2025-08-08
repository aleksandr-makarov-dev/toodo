using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toodo.Application.Issues.Commands.CreateIssueCommand;
using Toodo.Application.Issues.Commands.DeleteIssue;
using Toodo.Application.Issues.Commands.UpdateIssue;
using Toodo.Application.Issues.Queries.GetIssue;
using Toodo.Application.Issues.Queries.GetIssues;
using Toodo.Domain.Constants;
using Toodo.Domain.Entities;

namespace Toodo.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class IssuesController(
    IMediator mediator
) : ControllerBase
{
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> CreateIssue([FromBody] CreateIssueCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(new { id });
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetIssues()
    {
        var issues = await mediator.Send(new GetIssuesQuery());
        return Ok(issues);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetIssue([FromRoute] int id)
    {
        var issue = await mediator.Send(new GetIssueQuery(id));
        return Ok(issue);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateIssue([FromRoute] int id, [FromBody] UpdateIssueCommand command)
    {
        if (id != command.Id) return BadRequest();

        await mediator.Send(command);

        return NoContent();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteIssue([FromRoute] int id)
    {
        await mediator.Send(new DeleteIssueCommand(id));

        return NoContent();
    }
}