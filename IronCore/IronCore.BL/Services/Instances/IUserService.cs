using IronCore.BL.DTOs.User;

namespace IronCore.BL.Services.Instances;

public interface IUserService
{
    Task<UserGetDTO> GetUserByIdAsync(string id);
    Task<IEnumerable<UserGetDTO>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<IEnumerable<UserGetDTO>> GetAllUsersInChannelAsync(string channelId, CancellationToken cancellationToken);
    Task<LoginResponseDTO> LoginOrRegisterAsync(LoginDTO dto);
    Task<string> SetProfileImageAsync(ChangeProfileImageDTO dto, CancellationToken cancellationToken);
    Task LogoutAsync();
}