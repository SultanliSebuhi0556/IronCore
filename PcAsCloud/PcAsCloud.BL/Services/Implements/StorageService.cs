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

    public async Task<GetFileResultDTO> GetFileAsync(GetFileDTO dto, CancellationToken cancellationToken)
    {
        var user = await _getCurrentUserAsync();
        var folderName = dto.ChannelId != null ? Path.Combine("@ChannelFiles", dto.ChannelId) : user.UserName!;

        var storage = await _context.Storages.FirstOrDefaultAsync(x => x.Id.ToString() == dto.StorageId);
        if (storage == null) throw new NotFoundException<Storage>();

        var stream = await _fileHelper.GetFileAsync(folderName, storage.FileName, cancellationToken);
        return new() { Stream = stream, FileName = storage.FileName };
    }

    public async Task<UploadFileResultDTO> SaveFileAsync(UploadFileDTO dto, CancellationToken cancellationToken)
    {
        if (dto.File == null || dto.File.Length == 0) throw new Exception("No file uploaded"); //TODO: exc

        var user = await _getCurrentUserAsync();

        await using var stream = dto.File.OpenReadStream();

        string fileName = dto.File.FileName;

        if (!String.IsNullOrWhiteSpace(dto.NewFileName))
            fileName = dto.NewFileName + Path.GetExtension(dto.File.FileName);

        var newFileName = await _fileHelper.SaveFileAsync(dto.NewFolderName ?? user.UserName!, fileName, stream, null, cancellationToken);

        var storage = new Storage { AppUser = user, FileName = newFileName };
        await _context.Storages.AddAsync(storage, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new() { StorageId = storage.Id, FileName = newFileName };
    }

    public async Task DeleteFileAsync(DeleteFileDTO dto, CancellationToken cancellationToken)
    {
        var user = await _getCurrentUserAsync();

        var target = await _context.Storages.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.Id.ToString() == dto.StorageId, cancellationToken);
        if (target == null) throw new NotFoundException<Storage>();

        var folderName = dto.ChannelId != null ? Path.Combine("@ChannelFiles", dto.ChannelId) : user.UserName!;
        await _fileHelper.DeleteFileAsync(folderName, target.FileName);

        await Task.Run(() => _context.Remove(target));
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<AppUser> _getCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
        if (user == null) throw new NotFoundException<AppUser>();
        return user;
    }
}