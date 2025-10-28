
using SearchService.BL.MessageBroker.Interfaces;

namespace SearchService.API.Extensions;
public class RabbitMqConsumerBackgroundService(IServiceProvider _provider, ILogger<RabbitMqConsumerBackgroundService> _logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = _provider.CreateScope();
        var consumer = scope.ServiceProvider.GetRequiredService<IRabbitMqConsumer>();

        await Task.WhenAll(
           ConsumeQueue(consumer, "message.create", cancellationToken),
           ConsumeQueue(consumer, "message.delete", cancellationToken)
       );
    }

    private async Task ConsumeQueue(IRabbitMqConsumer consumer, string queue, CancellationToken cancellationToken)
    {
        await consumer.ConsumeMessagesAsync(queue, async message =>
        {
            _logger.LogInformation(message);
            await Task.CompletedTask;
        }, cancellationToken);
    }
}