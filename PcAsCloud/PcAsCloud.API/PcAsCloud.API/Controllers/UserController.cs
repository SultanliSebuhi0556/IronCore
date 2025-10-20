using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcAsCloud.API.Features.Commands.UserCommands.UserLoginOrRegister;
using PcAsCloud.API.Features.Commands.UserCommands.UserLogout;
using PcAsCloud.API.Features.Commands.UserCommands.UserSetProfileImage;
using PcAsCloud.API.Features.Queries.UserQueries.UserGetAll;
using PcAsCloud.API.Features.Queries.UserQueries.UserGetById;

namespace PcAsCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator _mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> LoginOrRegisterAndLogin([FromQuery] UserLoginOrRegisterRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> LoginOut([FromQuery] UserLogoutRequest request)
    {
        return Ok(await _mediator.Send(request));

    }

    [Authorize]
    [HttpPut("[action]")]
    public async Task<IActionResult> SetProfileImage([FromQuery] UserSetProfileImageRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetUserById([FromQuery] UserGetByIdRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllUsers([FromQuery] UserGetAllRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
}
