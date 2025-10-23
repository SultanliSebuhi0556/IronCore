using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.ChannelCommands.ChannelArchive;
public class ChannelArchiveHandler(IChannelServices _channelServices) : IRequestHandler<ChannelArchiveRequest, ChannelArchiveResponse>
{
    public async Task<ChannelArchiveResponse> Handle(ChannelArchiveRequest request, CancellationToken cancellationToken)
    {
        var result = await _channelServices.ArchiveUnarchiveChannelAsync(request.Id, cancellationToken);
        return new() { IsArchived = result };
    }
}