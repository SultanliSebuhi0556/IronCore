using PcAsCloud.BL.DTOs.User;

namespace PcAsCloud.BL.Services.Instances;

public interface IUserService
{
    Task<UserGetDTO> GetUserByIdAsync(string id);
    Task<IEnumerable<UserGetDTO>> GetAllUsersAsync();
    Task<IEnumerable<UserGetDTO>> GetAllUsersInChannelAsync(string channelId);
    Task<LoginResponseDTO> LoginOrRegisterAndLoginAsync(LoginDTO dto);
    Task SetProfileImageAsync(ChangeProfileImageDTO dto);
    Task LogoutAsync();
}