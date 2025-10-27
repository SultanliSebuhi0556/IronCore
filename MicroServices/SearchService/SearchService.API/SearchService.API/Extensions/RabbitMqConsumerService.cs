
using SearchService.BL.MessageBroker.Interfaces;

namespace SearchService.API.Extensions;
public class RabbitMqConsumerService : BackgroundService
{
    private readonly IServiceProvider _provider;
    private const string _queueName = "message";

    public RabbitMqConsumerService(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = _provider.CreateScope();
        var consumer = scope.ServiceProvider.GetRequiredService<IRabbitMqConsumer>();

        await consumer.ConsumeMessagesAsync(_queueName, async message =>
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Received: {message}");
            Console.ResetColor();
            await Task.CompletedTask;
        }, cancellationToken);
    }
}