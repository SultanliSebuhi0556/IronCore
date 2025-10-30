using MassTransit;
using Microsoft.Extensions.Logging;
using SearchService.BL.Services.Instances;
using SharedDTOs.DTOs;

namespace SearchService.BL.Consumers.MessageConsumers;
public class MessageDeleteConsumer(IMessageElasticService _service, ILogger<MessageCreateConsumer> _logger) : IConsumer<MessageDeleteDTO>
{
    public async Task Consume(ConsumeContext<MessageDeleteDTO> dto)
    {
        CancellationToken cancellationToken = new CancellationToken();
        await _service.RemoveAsync(dto.Message.Id, cancellationToken);
    }
}