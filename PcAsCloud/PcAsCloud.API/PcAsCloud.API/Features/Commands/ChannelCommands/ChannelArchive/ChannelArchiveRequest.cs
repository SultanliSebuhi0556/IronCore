using MediatR;

namespace PcAsCloud.API.Features.Commands.ChannelCommands.ChannelArchive;
public class ChannelArchiveRequest : IRequest<ChannelArchiveResponse>
{
    public string Id { get; set; }
}