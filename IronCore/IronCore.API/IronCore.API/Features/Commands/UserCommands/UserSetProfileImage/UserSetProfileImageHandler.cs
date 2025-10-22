using AutoMapper;
using IronCore.BL.DTOs.User;
using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.UserCommands.UserSetProfileImage;
public class UserSetProfileImageHandler(IUserService _userService, IMapper _mapper) : IRequestHandler<UserSetProfileImageRequest, UserSetProfileImageResponse>
{
    public async Task<UserSetProfileImageResponse> Handle(UserSetProfileImageRequest request, CancellationToken cancellationToken)
    {
        var imageUrl = await _userService.SetProfileImageAsync(_mapper.Map<ChangeProfileImageDTO>(request), cancellationToken);
        return new() { ImageUrl = imageUrl };
    }
}