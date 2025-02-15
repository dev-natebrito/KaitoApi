using Application.CalculateDamage.Haquerin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/weapons")]
public class WeaponController(IMediator mediator) : ControllerBase
{
    // [HttpPost("calculate")]
    // public async Task<IActionResult> CalculateDamage([FromBody] CalculateDamageCommand command)
    // {
    //     var result = await mediator.Send(command);
    //     return Ok(result);
    // }

    [HttpPost("haquerin")]
    public async Task<IActionResult> CalculateHaquerinDamage([FromBody] CalculateHaquerinDamageCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}