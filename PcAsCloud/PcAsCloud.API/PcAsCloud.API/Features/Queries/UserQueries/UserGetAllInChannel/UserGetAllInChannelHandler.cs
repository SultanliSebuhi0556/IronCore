using AutoMapper;
using MediatR;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Queries.UserQueries.UserGetAllInChannel;
public class UserGetAllInChannelHandler(IUserService _userService, IMapper _mapper) : IRequestHandler<UserGetAllInChannelRequest, IEnumerable<UserGetAllInChannelResponse>>
{
    public async Task<IEnumerable<UserGetAllInChannelResponse>> Handle(UserGetAllInChannelRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllUsersInChannelAsync(request.ChannelId);
        return _mapper.Map<IEnumerable<UserGetAllInChannelResponse>>(result);
    }
}