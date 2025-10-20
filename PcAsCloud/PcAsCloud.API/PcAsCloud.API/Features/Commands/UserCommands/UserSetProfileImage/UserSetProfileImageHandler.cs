using AutoMapper;
using MediatR;
using PcAsCloud.BL.DTOs.User;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Commands.UserCommands.UserSetProfileImage;
public class UserSetProfileImageHandler(IUserService _userService, IMapper _mapper) : IRequestHandler<UserSetProfileImageRequest, UserSetProfileImageResponse>
{
    public async Task<UserSetProfileImageResponse> Handle(UserSetProfileImageRequest request, CancellationToken cancellationToken)
    {
        await _userService.SetProfileImageAsync(_mapper.Map<ChangeProfileImageDTO>(request));
        return new();
    }
}