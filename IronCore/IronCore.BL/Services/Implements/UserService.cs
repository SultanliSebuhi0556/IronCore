using AutoMapper;
using IronCore.BL.DTOs.User;
using IronCore.BL.Exceptions.CommonExceptions;
using IronCore.BL.ExternalServices.Instances;
using IronCore.BL.Services.Instances;
using IronCore.CORE.Entities;
using IronCore.CORE.Enums;
using IronCore.DAL.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IronCore.BL.Services.Implements;

public class UserService(
    UserManager<AppUser> _userManager,
    AppDbContext _context,
    IHttpContextAccessor _httpContextAccessor,
    IWebHostEnvironment _webHostEnvironment,
    ITokenGenerator _tokenGenerator,
    SignInManager<AppUser> _signInManager,
    IMapper _mapper) : IUserService
{
    public async Task<IEnumerable<UserGetDTO>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var result = _mapper.Map<IEnumerable<UserGetDTO>>(await _userManager.Users.ToListAsync(cancellationToken));
        return result;
    }

    public async Task<IEnumerable<UserGetDTO>> GetAllUsersInChannelAsync(string channelId, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.Include(x => x.ChannelUsers).Where(x => x.ChannelUsers.Any(x => x.ChannelId.ToString() == channelId)).ToListAsync(cancellationToken);
        var result = _mapper.Map<IEnumerable<UserGetDTO>>(users);
        return result;
    }

    public async Task<UserGetDTO> GetUserByIdAsync(string id)
    {
        var result = await _userManager.FindByIdAsync(id);
        if (result == null) throw new NotFoundException<AppUser>();
        return _mapper.Map<UserGetDTO>(result);
    }

    public async Task<LoginResponseDTO> LoginOrRegisterAsync(LoginDTO dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null)
        {
            var newUser = _mapper.Map<AppUser>(dto);
            var path = Path.Combine("data", "profile_images", "default_profile_image.jpg");
            if (!File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, path))) throw new Exception("default profile image coulnt be found!"); //TODO: ex
            newUser.ProfileImageUrl = path;

            await _userManager.CreateAsync(newUser, dto.Password);
            if (newUser == null) throw new Exception(); //TODO: ex

            await _userManager.AddToRoleAsync(newUser, nameof(UserRoles.User));
            user = newUser;
        }

        var result = await _signInManager.PasswordSignInAsync(user!, dto.Password, true, false);
        if (!result.Succeeded) throw new Exception("IncorrectPasswordException"); //TODO: ex
        var token = _tokenGenerator.CreateJWTToken(user, 24);
        return new LoginResponseDTO
        {
            Id = user.Id.ToString(),
            Token = token,
        };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<string> SetProfileImageAsync(ChangeProfileImageDTO dto, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);
        if (user == null) throw new NotFoundException<AppUser>();

        var basePath = Path.Combine("data", "profile_images", "user_profile_images");
        var rootPath = Path.Combine(_webHostEnvironment.WebRootPath, basePath);
        if (!Path.Exists(rootPath)) Directory.CreateDirectory(rootPath);
        var uniqueName = $"{user.UserName!}_{Guid.NewGuid()}" + Path.GetExtension(dto.Image.FileName);
        var fullPath = Path.Combine(rootPath, uniqueName);

        using (Stream stream = new FileStream(fullPath, FileMode.Create))
            await dto.Image.CopyToAsync(stream);

        user.ProfileImageUrl = Path.Combine(basePath, uniqueName);
        await _context.SaveChangesAsync(cancellationToken);
        return user.ProfileImageUrl;
    }

    private async Task<AppUser> _getCurrentUserAsync()
    {
        var result = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (result == null) throw new NotFoundException<AppUser>();
        return result;
    }
}