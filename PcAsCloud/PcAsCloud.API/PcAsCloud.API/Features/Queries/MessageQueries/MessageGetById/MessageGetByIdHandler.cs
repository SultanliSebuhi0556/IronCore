using MediatR;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Queries.MessageQueries.MessageGetById;
public class MessageGetByIdHandler(IMessageService _messageService) : IRequestHandler<MessageGetByIdRequest, MessageGetByIdResponse>
{
    public async Task<MessageGetByIdResponse> Handle(MessageGetByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _messageService.GetMessageByIdAsync(request.Id);
        return new()
        {
            Id = result.Id,
            Content = result.Content,
            FileUrl = result.FileUrl,
            SendedByUserId = result.SendedById,
        };
    }
}