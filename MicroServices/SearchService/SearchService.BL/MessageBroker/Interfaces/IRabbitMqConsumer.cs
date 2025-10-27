namespace SearchService.BL.MessageBroker.Interfaces;
public interface IRabbitMqConsumer
{
    Task ConsumeMessagesAsync(string queueName, Func<string, Task> messageHandler, CancellationToken cancellationToken);

}