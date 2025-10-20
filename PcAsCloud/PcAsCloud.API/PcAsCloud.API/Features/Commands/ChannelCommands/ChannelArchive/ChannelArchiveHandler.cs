using MediatR;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Commands.ChannelCommands.ChannelArchive;
public class ChannelArchiveHandler(IChannelServices _channelServices) : IRequestHandler<ChannelArchiveRequest, ChannelArchiveResponse>
{
    public async Task<ChannelArchiveResponse> Handle(ChannelArchiveRequest request, CancellationToken cancellationToken)
    {
        var result = await _channelServices.ArchiveUnarchiveChannelAsync(request.Id, cancellationToken);
        return new() { IsArchived = result };
    }
}