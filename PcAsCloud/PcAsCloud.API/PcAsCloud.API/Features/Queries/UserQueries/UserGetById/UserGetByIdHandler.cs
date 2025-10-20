using MediatR;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Queries.UserQueries.UserGetById;
public class UserGetByIdHandler(IUserService _userService) : IRequestHandler<UserGetByIdRequest, UserGetByIdResponse>
{
    public async Task<UserGetByIdResponse> Handle(UserGetByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(request.Id);
        return new()
        {
            Id = user.Id,
            UserName = user.UserName,
            ProfileImageUrl = user.ProfileImageUrl
        };
    }
}