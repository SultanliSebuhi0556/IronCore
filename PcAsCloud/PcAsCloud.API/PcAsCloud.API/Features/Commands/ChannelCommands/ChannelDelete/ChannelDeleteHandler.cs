using MediatR;

namespace PcAsCloud.API.Features.Commands.ChannelCommands.ChannelDelete;
public class ChannelDeleteHandler : IRequestHandler<ChannelDeleteRequest, ChannelDeleteResponse>
{
    public Task<ChannelDeleteResponse> Handle(ChannelDeleteRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}