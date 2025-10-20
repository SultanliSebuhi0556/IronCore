using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcAsCloud.API.Features.Commands.StorageCommands.StorageDeleteFile;
using PcAsCloud.API.Features.Commands.StorageCommands.StorageUploadFile;
using PcAsCloud.API.Features.Queries.StorageQueries.StorageDownloadFile;

namespace PcAsCloud.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class StorageController(IMediator _mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFile(StorageUploadFileRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> DownloadFile(StorageDownloadFileRequest request)
    {
        var result = await _mediator.Send(request);
        result.Stream.Position = 0;
        return File(result.Stream, "application/octet-stream", request.FileName);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteFile(StorageDeleteFileRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
}