using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PcAsCloud.BL.DTOs.User;
using PcAsCloud.BL.Enums;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.ExternalServices.Instances;
using PcAsCloud.BL.Services.Instances;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.BL.Services.Implements;

public class UserService(
    UserManager<AppUser> _userManager,
    IHttpContextAccessor _httpContextAccessor,
    ITokenGenerator _tokenGenerator,
    SignInManager<AppUser> _signInManager,
    IMapper _mapper) : IUserService
{
    public async Task<IEnumerable<UserGetDTO>> GetAllUsersAsync()
    {
        var result = _mapper.Map<IEnumerable<UserGetDTO>>(await _userManager.Users.ToListAsync());
        return result;
    }

    public async Task<UserGetDTO> GetUserByIdAsync(string id)
    {
        var result = await _userManager.FindByIdAsync(id);
        if (result == null) throw new NotFoundException<AppUser>();
        return _mapper.Map<UserGetDTO>(result);
    }

    public async Task<LoginResponseDTO> LoginOrRegisterAndLoginAsync(LoginDTO dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null)
        {
            var newUser = _mapper.Map<AppUser>(dto);
            newUser.ProfileImageUrl = ""; //TODO:default user profile image!

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

    public Task SetProfileImageAsync(string userId, IFormFile image)
    {
        throw new NotImplementedException();
    }

    private async Task<AppUser> _getCurrentUserAsync()
    {
        var result = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (result == null) throw new NotFoundException<AppUser>();
        return result;
    }
}