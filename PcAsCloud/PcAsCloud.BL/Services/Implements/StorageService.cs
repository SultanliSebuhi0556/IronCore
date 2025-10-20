using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PcAsCloud.BL.DTOs.Storage;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.ExternalServices.Instances;
using PcAsCloud.BL.Services.Services.Instances;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.BL.Services.Services.Implements;
public class StorageService : IStorageService
{
    private readonly IFileHelper _fileHelper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StorageService(IFileHelper fileHelper, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _fileHelper = fileHelper;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<MemoryStream> GetFileAsync(string fileName, CancellationToken cancellationToken)
    {
        var user = await _getCurrentUserAsync();
        return await _fileHelper.GetFileAsync(user.UserName!, fileName, cancellationToken);
    }

    public async Task<string> SaveFileAsync(UploadFileDTO dto, CancellationToken cancellationToken)
    {
        if (dto.File == null || dto.File.Length == 0) throw new Exception("No file uploaded"); //TODO: exc

        var user = await _getCurrentUserAsync();

        await using var stream = dto.File.OpenReadStream();

        string fileName = dto.File.FileName;

        if (!String.IsNullOrWhiteSpace(dto.NewFileName))
            fileName = dto.NewFileName + Path.GetExtension(dto.File.FileName);

        return await _fileHelper.SaveFileAsync(user.UserName!, fileName, stream, cancellationToken);
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var user = await _getCurrentUserAsync();
        await _fileHelper.DeleteFileAsync(user.UserName!, fileName);
    }

    private async Task<AppUser> _getCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
        if (user == null) throw new NotFoundException<AppUser>();
        return user;
    }
}