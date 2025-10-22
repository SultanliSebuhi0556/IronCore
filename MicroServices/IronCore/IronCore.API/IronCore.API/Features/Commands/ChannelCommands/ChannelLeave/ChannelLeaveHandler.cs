using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.ChannelCommands.ChannelLeave;
public class ChannelLeaveHandler(IChannelServices _channelServices) : IRequestHandler<ChannelLeaveRequest, ChannelLeaveResponse>
{
    public async Task<ChannelLeaveResponse> Handle(ChannelLeaveRequest request, CancellationToken cancellationToken)
    {
        await _channelServices.LeaveChannelAsync(request.Id, cancellationToken);
        return new();
    }
}