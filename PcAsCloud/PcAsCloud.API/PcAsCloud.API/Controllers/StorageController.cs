using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcAsCloud.BL.DTOs.Storage;
using PcAsCloud.BL.Services.Services.Instances;

namespace PcAsCloud.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class StorageController(
    IStorageService _storageService,
    IValidator<DeleteFileDTO> _deleteValidator,
    IValidator<DownloadFileDTO> _downloadValidator,
    IValidator<UploadFileDTO> _uploadValidator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFile(UploadFileDTO dto)
    {
        await _uploadValidator.ValidateAndThrowAsync(dto);
        var cancellationToken = new CancellationToken();
        return Ok(await _storageService.SaveFileAsync(dto.File, dto.NewFileName, cancellationToken));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> DownloadFile(DownloadFileDTO dto)
    {
        await _downloadValidator.ValidateAndThrowAsync(dto);
        var cancellationToken = new CancellationToken();
        var result = await _storageService.GetFileAsync(dto.FileName, cancellationToken);

        result.Position = 0;
        return File(result, "application/octet-stream", dto.FileName);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteFile(DeleteFileDTO dto)
    {
        await _deleteValidator.ValidateAndThrowAsync(dto);
        await _storageService.DeleteFileAsync(dto.FileName);
        return Ok();
    }
}