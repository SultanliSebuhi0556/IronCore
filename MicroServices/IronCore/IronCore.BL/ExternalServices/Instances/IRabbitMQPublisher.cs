namespace IronCore.BL.ExternalServices.Instances;
public interface IRabbitMQPublisher
{
    Task PublishMessagesAsync(string message, string routingKey, CancellationToken cancellationToken);
}