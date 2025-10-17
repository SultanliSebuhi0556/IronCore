using MediatR;

namespace PcAsCloud.API.Features.Queries.ChannelQueries.ChannelGetById;
public class ChannelGetByIdHandler : IRequestHandler<ChannelGetByIdRequest, ChannelGetByIdResponse>
{
    public Task<ChannelGetByIdResponse> Handle(ChannelGetByIdRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}