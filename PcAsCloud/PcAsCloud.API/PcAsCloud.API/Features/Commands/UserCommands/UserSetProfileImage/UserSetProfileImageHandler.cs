using MediatR;

namespace PcAsCloud.API.Features.Commands.UserCommands.UserSetProfileImage;
public class UserSetProfileImageHandler : IRequestHandler<UserSetProfileImageRequest, UserSetProfileImageResponse>
{
    public Task<UserSetProfileImageResponse> Handle(UserSetProfileImageRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}