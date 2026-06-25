using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CryptoAIPlatform.Application.Research;
using CryptoAIPlatform.Domain.Research;

namespace CryptoAIPlatform.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/research")]
[ApiVersion("1.0")]
[Authorize]
public class ResearchController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResearchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResearchStudy), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateStudy([FromBody] CreateResearchStudyCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetStudy), new { studyId = result.Id }, result);
    }

    [HttpPost("{studyId:guid}/execute")]
    [ProducesResponseType(typeof(ResearchResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ExecuteStudy(Guid studyId)
    {
        var result = await _mediator.Send(new ExecuteResearchStudyCommand(studyId));
        return Ok(result);
    }

    [HttpGet("{studyId:guid}")]
    [ProducesResponseType(typeof(ResearchStudy), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStudy(Guid studyId)
    {
        var result = await _mediator.Send(new GetResearchStudyQuery(studyId));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ResearchStudy>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStudies([FromQuery] Guid userId)
    {
        var result = await _mediator.Send(new GetAllResearchStudiesQuery(userId));
        return Ok(result);
    }
}
