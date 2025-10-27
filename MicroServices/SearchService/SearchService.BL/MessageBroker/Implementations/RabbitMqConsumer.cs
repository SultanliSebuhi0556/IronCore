using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SearchService.BL.MessageBroker.Interfaces;
using SearchService.BL.Services.Instances;
using SearchService.CORE.Entities;
using System.Text;
using System.Text.Json;

namespace SearchService.BL.MessageBroker.Implementations;

public class RabbitMqConsumer(IMessageElasticService _messageService, IChannel _channel) : IRabbitMqConsumer
{
    public async Task ConsumeMessagesAsync(string queueName, Func<string, Task> messageHandler, CancellationToken cancellationToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, eventArgs) =>
        {
            var messageJson = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            try
            {
                var message = JsonSerializer.Deserialize<Message>(messageJson);
                if (message != null)
                {
                    await _messageService.AddOrUpdateMessageAsync(message, cancellationToken);
                    await messageHandler(messageJson);
                }
                await _channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                await Task.Delay(3000);
                await _channel.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: true, cancellationToken);
            }
        };
        await _channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer, cancellationToken);
    }
}