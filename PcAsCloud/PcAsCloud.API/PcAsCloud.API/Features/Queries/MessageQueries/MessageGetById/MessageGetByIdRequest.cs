using MediatR;

namespace PcAsCloud.API.Features.Queries.MessageQueries.MessageGetById;
public class MessageGetByIdRequest : IRequest<MessageGetByIdResponse>
{
    public string Id { get; set; }
}