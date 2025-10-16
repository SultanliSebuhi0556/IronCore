using MediatR;

namespace PcAsCloud.API.Features.Commands.ChannelCommands.ChannelArchive;
public class ChannelArchiveHandler : IRequestHandler<ChannelArchiveRequest, ChannelArchiveResponse>
{
    public Task<ChannelArchiveResponse> Handle(ChannelArchiveRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}