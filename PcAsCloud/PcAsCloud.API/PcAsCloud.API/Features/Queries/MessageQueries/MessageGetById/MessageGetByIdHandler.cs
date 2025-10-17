using MediatR;

namespace PcAsCloud.API.Features.Queries.MessageQueries.MessageGetById;
public class MessageGetByIdHandler : IRequestHandler<MessageGetByIdRequest, MessageGetByIdResponse>
{
    public Task<MessageGetByIdResponse> Handle(MessageGetByIdRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}