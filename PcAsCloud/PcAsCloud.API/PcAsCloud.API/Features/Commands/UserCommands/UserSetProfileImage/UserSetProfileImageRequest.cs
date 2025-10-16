using MediatR;

namespace PcAsCloud.API.Features.Commands.UserCommands.UserSetProfileImage;
public class UserSetProfileImageRequest : IRequest<UserSetProfileImageResponse>
{
    public string Id { get; set; }
    public IFormFile Image { get; set; }
}