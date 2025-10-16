using MediatR;

namespace PcAsCloud.API.Features.Queries.UserQueries.UserGetAllInChannel;
public class UserGetAllInChannelHandler : IRequestHandler<UserGetAllInChannelRequest, IEnumerable<UserGetAllInChannelResponse>>
{
    public Task<IEnumerable<UserGetAllInChannelResponse>> Handle(UserGetAllInChannelRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}