using MediatR;

namespace IronCore.API.Features.Commands.ChannelCommands.ChannelLeave;
public class ChannelLeaveRequest : IRequest<ChannelLeaveResponse>
{
    public string Id { get; set; }
}