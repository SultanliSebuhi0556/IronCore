using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcAsCloud.BL.DTOs.User;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService _userService) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> LoginOrRegisterAndLogin([FromQuery] LoginDTO dto)
    {
        return Ok(await _userService.LoginOrRegisterAndLoginAsync(dto));
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> LoginOut()
    {
        await _userService.LogoutAsync();
        return Ok();
    }

    [Authorize]
    [HttpPut("[action]")]
    public async Task<IActionResult> SetProfileImage(string id, IFormFile image)
    {
        await _userService.SetProfileImageAsync(id, image);
        return Ok();
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetUserById([FromQuery] string id)
    {
        return Ok(await _userService.GetUserByIdAsync(id));
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }
}
