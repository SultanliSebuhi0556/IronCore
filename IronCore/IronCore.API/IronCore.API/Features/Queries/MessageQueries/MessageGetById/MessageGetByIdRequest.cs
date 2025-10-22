using MediatR;

namespace IronCore.API.Features.Queries.MessageQueries.MessageGetById;
public class MessageGetByIdRequest : IRequest<MessageGetByIdResponse>
{
    public string Id { get; set; }
}