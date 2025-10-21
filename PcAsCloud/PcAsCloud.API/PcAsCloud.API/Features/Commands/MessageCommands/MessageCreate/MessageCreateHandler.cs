using AutoMapper;
using MediatR;
using PcAsCloud.BL.DTOs.Message;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Commands.MessageCommands.MessageCreate;
public class MessageCreateHandler(IMessageService _messageService, IMapper _mapper) : IRequestHandler<MessageCreateRequest, MessageCreateResponse>
{
    public async Task<MessageCreateResponse> Handle(MessageCreateRequest request, CancellationToken cancellationToken)
    {
        var result = await _messageService.CreateMessageAsync(_mapper.Map<MessageCreateDTO>(request), cancellationToken);
        return new() { Id = result!.Id, StorageId = result.StorageId, FileName = result.FileName };
    }
}