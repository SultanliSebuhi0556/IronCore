using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.ChannelCommands.ChannelJoin;
public class ChannelJoinHandler(IChannelServices _channelServices) : IRequestHandler<ChannelJoinRequest, ChannelJoinResponse>
{
    public async Task<ChannelJoinResponse> Handle(ChannelJoinRequest request, CancellationToken cancellationToken)
    {
        await _channelServices.JoinChannelAsync(request.Id, cancellationToken);
        return new();
    }
}