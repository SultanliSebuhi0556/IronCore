using MassTransit;
using Microsoft.Extensions.Logging;
using SharedDTOs.DTOs;

namespace SearchService.BL.Consumers;
public class MessageConsumer(ILogger<MessageConsumer> _logger) : IConsumer<MessageDTO>
{
    public Task Consume(ConsumeContext<MessageDTO> context)
    {
        _logger.LogInformation($"{nameof(MessageConsumer)}: {context.Message}");
        return Task.CompletedTask;
    }
}