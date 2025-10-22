using IronCore.API.Features.Commands.StorageCommands.StorageDeleteFile;
using IronCore.API.Features.Commands.StorageCommands.StorageUploadFile;
using IronCore.API.Features.Queries.StorageQueries.StorageDownloadFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IronCore.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class StorageController(IMediator _mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFile([FromQuery] StorageUploadFileRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> DownloadFile([FromQuery] StorageDownloadFileRequest request)
    {
        var result = await _mediator.Send(request);
        result.Stream.Position = 0;
        return File(result.Stream, "application/octet-stream", result.FileName);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteFile([FromQuery] StorageDeleteFileRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
}