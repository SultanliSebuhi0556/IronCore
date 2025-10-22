using MediatR;

namespace IronCore.API.Features.Queries.ChannelQueries.ChannelGetById;
public class ChannelGetByIdRequest : IRequest<ChannelGetByIdResponse>
{
    public string Id { get; set; }
}