using Microsoft.AspNetCore.Http;
using PcAsCloud.BL.DTOs.User;

namespace PcAsCloud.BL.Services.Instances;

public interface IUserService
{
    Task<UserGetDTO> GetUserByIdAsync(string id);
    Task<IEnumerable<UserGetDTO>> GetAllUsersAsync();
    Task<LoginResponseDTO> LoginOrRegisterAndLoginAsync(LoginDTO dto);
    Task SetProfileImageAsync(string userId, IFormFile image);
    Task LogoutAsync();
}