using AutoMapper;
using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Queries.UserQueries.UserGetAllInChannel;
public class UserGetAllInChannelHandler(IUserService _userService, IMapper _mapper) : IRequestHandler<UserGetAllInChannelRequest, IEnumerable<UserGetAllInChannelResponse>>
{
    public async Task<IEnumerable<UserGetAllInChannelResponse>> Handle(UserGetAllInChannelRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllUsersInChannelAsync(request.ChannelId, cancellationToken);
        return _mapper.Map<IEnumerable<UserGetAllInChannelResponse>>(result);
    }
}