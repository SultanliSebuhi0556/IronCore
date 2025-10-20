using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PcAsCloud.BL.DTOs.Storage;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.ExternalServices.Instances;
using PcAsCloud.BL.Services.Services.Instances;
using PcAsCloud.CORE.Entities;
using PcAsCloud.DAL.Context;

namespace PcAsCloud.BL.Services.Services.Implements;
public class StorageService : IStorageService
{
    private readonly IFileHelper _fileHelper;
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StorageService(IFileHelper fileHelper, UserManager<AppUser> userManager, AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _fileHelper = fileHelper;
        _userManager = userManager;
        _context = context;
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

        await _context.Storages.AddAsync(new Storage { AppUser = user, FileName = fileName }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return await _fileHelper.SaveFileAsync(user.UserName!, fileName, stream, null, cancellationToken);
    }

    public async Task DeleteFileAsync(string fileName, CancellationToken cancellationToken)
    {
        var user = await _getCurrentUserAsync();

        var target = await _context.Storages.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.AppUser == user && x.FileName == fileName, cancellationToken);
        if (target == null) throw new NotFoundException<Storage>();

        await Task.Run(() => _context.Remove(target));
        await _context.SaveChangesAsync(cancellationToken);

        await _fileHelper.DeleteFileAsync(user.UserName!, fileName);
    }

    private async Task<AppUser> _getCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
        if (user == null) throw new NotFoundException<AppUser>();
        return user;
    }
}