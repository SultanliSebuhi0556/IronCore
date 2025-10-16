using MediatR;

namespace PcAsCloud.API.Features.Commands.ChannelCommands.ChannelCreate;
public class ChannelCreateHandler : IRequestHandler<ChannelCreateRequest, ChannelCreateResponse>
{
    public Task<ChannelCreateResponse> Handle(ChannelCreateRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}