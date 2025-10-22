using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Queries.ChannelQueries.ChannelGetById;
public class ChannelGetByIdHandler(IChannelServices _channelServices) : IRequestHandler<ChannelGetByIdRequest, ChannelGetByIdResponse>
{
    public async Task<ChannelGetByIdResponse> Handle(ChannelGetByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _channelServices.GetChannelByIdAsync(request.Id, cancellationToken);
        return new()
        {
            IsDirect = result.IsDirect,
            Name = result.Name,
            Id = result.Id,
            UserIds = result.UserIds
        };
    }
}