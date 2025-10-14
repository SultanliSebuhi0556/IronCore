using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PcAsCloud.BL.DTOs.Storage;
using PcAsCloud.BL.Services.Services.Instances;

namespace PcAsCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StorageController(
    IStorageService _storageService,
    IValidator<DeleteFileDTO> _deleteValidator,
    IValidator<DownloadFileDTO> _dowloadValidator,
    IValidator<UploadFileDTO> _uploadValidator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFile(UploadFileDTO dto)
    {
        await _uploadValidator.ValidateAndThrowAsync(dto);
        var cancellationToken = new CancellationToken();
        await _storageService.SaveFileAsync(dto.File, dto.NewFileName, cancellationToken);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> DownloadFile(DownloadFileDTO dto)
    {
        await _dowloadValidator.ValidateAndThrowAsync(dto);
        var cancellationToken = new CancellationToken();
        var result = await _storageService.GetFileAsync(dto.FileName, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteFile(DeleteFileDTO dto)
    {
        await _deleteValidator.ValidateAndThrowAsync(dto);
        await _storageService.DeleteFileAsync(dto.FileName);
        return Ok();
    }
}