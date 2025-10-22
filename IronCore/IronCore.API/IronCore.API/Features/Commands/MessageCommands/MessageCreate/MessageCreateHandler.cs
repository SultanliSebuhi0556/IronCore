using AutoMapper;
using IronCore.BL.DTOs.Message;
using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.MessageCommands.MessageCreate;
public class MessageCreateHandler(IMessageService _messageService, IMapper _mapper) : IRequestHandler<MessageCreateRequest, MessageCreateResponse>
{
    public async Task<MessageCreateResponse> Handle(MessageCreateRequest request, CancellationToken cancellationToken)
    {
        var result = await _messageService.CreateMessageAsync(_mapper.Map<MessageCreateDTO>(request), cancellationToken);
        return new() { Id = result!.Id, StorageId = result.StorageId, FileName = result.FileName };
    }
}