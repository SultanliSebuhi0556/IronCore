using MediatR;

namespace PcAsCloud.API.Features.Commands.UserCommands.UserLogout;
public class UserLogoutHandler : IRequestHandler<UserLogoutRequest, UserLogoutResponse>
{
    public Task<UserLogoutResponse> Handle(UserLogoutRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}