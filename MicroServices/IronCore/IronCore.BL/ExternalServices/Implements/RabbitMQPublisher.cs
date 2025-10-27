using IronCore.BL.ExternalServices.Instances;
using RabbitMQ.Client;
using System.Text;

namespace IronCore.BL.ExternalServices.Implements;
public class RabbitMQPublisher(IChannel _channel) : IRabbitMQPublisher
{
    public async Task PublishMessagesAsync(string message, string routingKey, CancellationToken cancellationToken)
    {
        var body = Encoding.UTF8.GetBytes(message);
        await _channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: routingKey,
            mandatory: true,
            basicProperties: new BasicProperties { Persistent = true }, body: body, cancellationToken);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Message sent - {message}");
        Console.ResetColor();
    }
}