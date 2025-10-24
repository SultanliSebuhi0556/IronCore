using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SearchService.CORE.Options;
using System.Text;

namespace SearchService.API.MessageBrokers.Implementations;

public class RabbitMqConsumer(IOptions<RabbitMQOptions> _options)
{
    private protected string queueName = "messagesQueue";
    public async Task SendMessage()
    {
        ConnectionFactory factory = new();
        factory.Uri = new(_options.Value.Uri);
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (sender, eventArgs) =>
        {
            byte[] body = eventArgs.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
        };

        await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer);
    }
}
