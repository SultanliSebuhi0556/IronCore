using MediatR;

namespace PcAsCloud.API.Features.Commands.UserCommands.UserLoginOrRegister;
public class UserLoginOrRegisterHandler : IRequestHandler<UserLoginOrRegisterRequest, UserLoginOrRegisterResponse>
{
    public Task<UserLoginOrRegisterResponse> Handle(UserLoginOrRegisterRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}