using MediatR;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Commands.ChannelCommands.ChannelJoin;
public class ChannelJoinHandler(IChannelServices _channelServices) : IRequestHandler<ChannelJoinRequest, ChannelJoinResponse>
{
    public async Task<ChannelJoinResponse> Handle(ChannelJoinRequest request, CancellationToken cancellationToken)
    {
        await _channelServices.JoinChannelAsync(request.Id);
        return new();
    }
}