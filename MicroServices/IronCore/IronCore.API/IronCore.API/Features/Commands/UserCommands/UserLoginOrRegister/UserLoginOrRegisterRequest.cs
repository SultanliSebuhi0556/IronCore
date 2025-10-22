using MediatR;

namespace IronCore.API.Features.Commands.UserCommands.UserLoginOrRegister;
public class UserLoginOrRegisterRequest : IRequest<UserLoginOrRegisterResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}