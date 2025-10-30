using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using SearchService.BL.Services.Instances;
using SearchService.CORE.Entities;
using SharedDTOs.DTOs;

namespace SearchService.BL.Consumers.MessageConsumers;
public class MessageCreateConsumer(IMessageElasticService _service, IMapper _mapper, ILogger<MessageCreateConsumer> _logger) : IConsumer<MessageCreateDTO>
{
    public async Task Consume(ConsumeContext<MessageCreateDTO> dto)
    {
        CancellationToken cancellationToken = new CancellationToken();
        await _service.AddOrUpdateMessageAsync(_mapper.Map<Message>(dto.Message), cancellationToken);
    }
}
