using MediatR;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Commands.UserCommands.UserLogout;
public class UserLogoutHandler(IUserService _userService) : IRequestHandler<UserLogoutRequest, UserLogoutResponse>
{
    public async Task<UserLogoutResponse> Handle(UserLogoutRequest request, CancellationToken cancellationToken)
    {
        await _userService.LogoutAsync();
        return new();
    }
}