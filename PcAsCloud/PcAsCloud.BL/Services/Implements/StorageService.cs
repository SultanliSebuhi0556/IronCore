using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.Helpers.Instances;
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

    public async Task SaveFileAsync(IFormFile file, string? newFileName, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0) throw new Exception("No file uploaded"); //TODO: exc

        var user = await _getCurrentUserAsync();

        await using var stream = file.OpenReadStream();
        string fileName = newFileName ?? file.FileName;

        await _fileHelper.SaveFileAsync(user.UserName!, file.FileName, stream, cancellationToken);
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