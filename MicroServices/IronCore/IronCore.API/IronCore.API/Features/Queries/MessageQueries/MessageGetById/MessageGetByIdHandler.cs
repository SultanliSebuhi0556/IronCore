using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Queries.MessageQueries.MessageGetById;
public class MessageGetByIdHandler(IMessageService _messageService) : IRequestHandler<MessageGetByIdRequest, MessageGetByIdResponse>
{
    public async Task<MessageGetByIdResponse> Handle(MessageGetByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _messageService.GetMessageByIdAsync(request.Id, cancellationToken);
        return new()
        {
            Id = result.Id,
            Content = result.Content,
            StorageId = result.StorageId,
            IsRead = result.IsRead,
            SendedByUserId = result.SendedById,
        };
    }
}