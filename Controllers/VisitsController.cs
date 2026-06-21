using Microsoft.AspNetCore.Mvc;
using s6464_KolokwiumPoprawa.Dto;
using s6464_KolokwiumPoprawa.Services;

namespace s6464_KolokwiumPoprawa.Controllers;

[ApiController]
[Route("api/visits")]
public class VisitsController(IVisitService visitService) : ControllerBase
{
    [HttpGet("{visitId:int}")]
    [ProducesResponseType<VisitResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VisitResponseDto>> GetVisit(
        int visitId,
        CancellationToken cancellationToken)
    {
        var visit = await visitService.GetVisitAsync(visitId, cancellationToken);

        if (visit is null)
        {
            return NotFound(new { message = $"Visit with id {visitId} does not exist." });
        }

        return Ok(visit);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateVisit(
        [FromBody] CreateVisitDto request,
        CancellationToken cancellationToken)
    {
        var result = await visitService.CreateVisitAsync(request, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }

        return CreatedAtAction(
            nameof(GetVisit),
            new { visitId = result.VisitId!.Value },
            new { visitId = result.VisitId.Value });
    }
}

