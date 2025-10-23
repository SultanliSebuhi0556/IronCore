using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.ChannelCommands.ChannelDelete;
public class ChannelDeleteHandler(IChannelServices _channelServices) : IRequestHandler<ChannelDeleteRequest, ChannelDeleteResponse>
{
    public async Task<ChannelDeleteResponse> Handle(ChannelDeleteRequest request, CancellationToken cancellationToken)
    {
        await _channelServices.DeleteChannelAsync(request.Id, cancellationToken);
        return new();
    }
}