using AutoMapper;
using MediatR;
using PcAsCloud.BL.DTOs.User;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Commands.UserCommands.UserLoginOrRegister;
public class UserLoginOrRegisterHandler(IUserService _userService, IMapper _mapper) : IRequestHandler<UserLoginOrRegisterRequest, UserLoginOrRegisterResponse>
{
    public async Task<UserLoginOrRegisterResponse> Handle(UserLoginOrRegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.LoginOrRegisterAndLoginAsync(_mapper.Map<LoginDTO>(request));
        return new() { Id = result.Id, Token = result.Token };
    }
}