using AutoMapper;
using IronCore.BL.DTOs.User;
using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.UserCommands.UserLoginOrRegister;
public class UserLoginOrRegisterHandler(IUserService _userService, IMapper _mapper) : IRequestHandler<UserLoginOrRegisterRequest, UserLoginOrRegisterResponse>
{
    public async Task<UserLoginOrRegisterResponse> Handle(UserLoginOrRegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.LoginOrRegisterAsync(_mapper.Map<LoginDTO>(request));
        return new() { Id = result.Id, Token = result.Token };
    }
}