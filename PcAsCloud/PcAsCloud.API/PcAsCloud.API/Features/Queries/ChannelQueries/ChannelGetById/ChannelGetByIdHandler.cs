using MediatR;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Queries.ChannelQueries.ChannelGetById;
public class ChannelGetByIdHandler(IChannelServices _channelServices) : IRequestHandler<ChannelGetByIdRequest, ChannelGetByIdResponse>
{
    public async Task<ChannelGetByIdResponse> Handle(ChannelGetByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _channelServices.GetChannelByIdAsync(request.Id);
        return new()
        {
            IsDirect = result.IsDirect,
            Name = result.Name,
            Id = result.Id,
            UserIds = result.UserIds
        };
    }
}